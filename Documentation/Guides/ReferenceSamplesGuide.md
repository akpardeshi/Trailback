# Table of Contents

1. [Introduction](#introduction)
2. [Trailback UGUI Demo](#trailback-ugui-demo)
3. [Legacy Input](#legacy-input)
4. [Unity Input System](#unity-input-system)
5. [Runtime Monitor](#runtime-monitor)
6. [Next Steps](#next-steps)

---

# Introduction

The Reference Samples Guide walks through the sample projects included with Trailback and explains what each one is designed to demonstrate.

In addition to demonstrating Trailback's workflows, this guide includes complete reference implementations that you can use as the starting point for your own integrations.

While the **Features Guide** explains what Trailback can do, this guide shows those features in working examples. Each sample highlights a practical integration or workflow that you can explore, learn from, and adapt to your own project.

The samples are optional, but they're a great way to see Trailback in action without starting from scratch. Whether you're evaluating the framework, looking for a specific integration example, or deciding how to structure your own project, the included samples provide a practical starting point.

---

# Trailback UGUI Demo

The **Trailback UGUI Demo** is the best place to get familiar with Trailback.

It brings the framework's core features together in a single scene, giving you a chance to see how navigation behaves in a complete UI instead of isolated examples. Most of the reference samples in this guide build on the same project, so spending a few minutes with the demo makes the rest of the documentation much easier to follow.

You'll also find the reference implementations for **Legacy Input**, the **Unity Input System**, and the **Runtime Monitor** inside the **Trailback UGUI Demo** sample. The **Trailback UGUI Demo** serves as the primary reference implementation for Trailback and is referenced throughout the documentation.

Each implementation can be explored directly in the demo scene and through the accompanying scripts included with the imported sample.

> [!TIP]
>
> If you're new to Trailback, start with the **Trailback UGUI Demo** before exploring the individual reference samples. Seeing the full navigation flow first makes it easier to understand how each sample fits into the overall framework.

---

## Included Content

The demo is organized around three areas of the framework.

### Core Features

The following features are included in the demo:

* Navigation History
* Root Protection
* Navigation Root Reached
* Navigation Categories
* Navigation Blockers

### Reference Samples

The demo includes working examples of:

* Legacy Input
* Unity Input System
* Runtime Monitor

---

## Recommended Learning Path

If this is your first time using Trailback, the following order works well.

```text
Trailback UGUI Demo
        │
        ├── Explore the navigation flow
        │
        ├── Observe Navigation History
        │
        ├── Test Root Protection
        │
        ├── Trigger the Navigation Root Reached event
        │
        ├── Open and close popups
        │
        ├── Inspect the Runtime Monitor
        │
        ├── Switch between Legacy Input and the Unity Input System
        │
        └── Reload the scene and confirm navigation continues to work
```

By the end of this walkthrough, you'll have seen the framework's core navigation features, optional integrations, and scene management workflow in a single project.

---

> [!NOTE]
> 
> The Legacy Input and Unity Input System samples are almost identical. The only thing that changes is how the back request is detected. After `RaiseBackRequested()` is called, Trailback processes the request in exactly the same way.

# Legacy Input

**Overview**

The Legacy Input sample shows how to integrate Trailback using Unity's built-in **Legacy Input Manager**.

It's the default input setup used by the **Trailback UGUI Demo**, so you can explore Trailback without changing the demo or configuring another input system.

If your project already uses the Legacy Input Manager, this sample demonstrates the complete integration workflow.

**Requirements**

Make sure your project is using Unity's **Legacy Input Manager**.

```text
Edit
    → Project Settings
        → Player
            → Active Input Handling
                → Input Manager (Old)
```

**Implementation**
`LegacyBackInputSource` listens for back input using Unity's built-in Legacy Input Manager. When the configured key is pressed, it raises the `BackRequested` event. From there, your Navigation Controller forwards the request to Trailback, and the rest of the navigation flow continues as normal.

```csharp
    public class LegacyBackInputSource : BackInputSource
    {
        [Tooltip("Pressing this key raises a back navigation request.")] [SerializeField]
        private KeyCode backKey = KeyCode.Escape;

        /// <summary>
        /// Monitors the configured key and raises a back navigation request when pressed.
        /// </summary>
        private void Update()
        {
            if (Input.GetKeyDown(backKey))
            {
                RaiseBackRequested();
            }
        }
    }
```

**How It Works**
The implementation follows the same pattern used by every BackInputSource.

RaiseBackRequested() is provided by the BackInputSource base class. It raises the BackRequested event, allowing every input implementation to communicate with Trailback through the same event regardless of how the input was detected.

```text
Escape Key
      ↓
LegacyBackInputSource
      ↓
RaiseBackRequested()
      ↓
BackRequested Event
      ↓
Navigation Controller
      ↓
Trailback
```

`LegacyBackInputSource` has a single job: detect back input and notify the rest of the navigation system.

It doesn't execute navigation, update navigation history, or manage your UI. Once it raises `BackRequested`, the Navigation Controller and Trailback take care of everything else.

**Configuration**
To use `LegacyBackInputSource`:

1. Add the component to a GameObject in your scene.
2. Choose the key you want to use for Back navigation.
3. Assign the component to your Navigation Controller.
4. Subscribe to the `BackRequested` event, or connect it through the Inspector as shown in the README.

No additional packages or setup are required.

Once everything is connected, pressing the configured key raises a `BackRequested` event.

Your Navigation Controller receives the event and forwards it to Trailback, where the normal navigation pipeline takes over.

The **Trailback UGUI Demo** includes a complete working implementation of this integration if you'd like to see it in action.

> [!NOTE]
>
> Some Unity versions recommend switching to the Unity Input System. These messages come from Unity and aren't related to Trailback. The Legacy Input sample is fully supported and continues to work as expected.

The default Back key is Escape, but you can assign any KeyCode that fits your project's input scheme.
 
The **Trailback UGUI Demo** listens for `BackRequested` events from `DemoNavigationController`, but that's just one possible approach. You're free to connect the input source to whatever navigation controller or architecture your project already uses.

---

# Unity Input System

**Overview**

The Unity Input System sample shows how to integrate Trailback using Unity's **Input System** package.

The overall integration is the same as the Legacy Input sample. The only difference is the input source. `InputSystemBackInputSource` listens for Input Actions and raises the same `BackRequested` event, so the rest of your navigation code doesn't need to change. Because both input sources raise the same `BackRequested` event, switching between them doesn't require changes to your Navigation Controller or Trailback integration.

If your project already uses the Unity Input System, this sample demonstrates the complete integration workflow.

**Requirements**

Before using this sample, make sure your project is configured to use the Unity Input System.

* The **Unity Input System** package is installed.
* **Active Input Handling** is set to **Input System Package (New)**.

```text
Edit
    → Project Settings
        → Player
            → Active Input Handling
                → Input System Package (New)
```

**Implementation**
`InputSystemBackInputSource` follows the same integration pattern as `LegacyBackInputSource`. The only difference is how the back request is detected.

Instead of polling a key every frame, it listens for a Unity Input System **Input Action**. When that action is performed, the component raises the `BackRequested` event.

```csharp
public class InputSystemBackInputSource : BackInputSource
    {
        #region Configuration

        [Tooltip("Input System action that triggers a back navigation request when performed.")]
        [SerializeField]
        private InputActionReference backAction;
        
        #endregion
        
        
        #region Unity Lifecycle

        /// <summary>
        /// Subscribes to the configured input action when the component becomes active.
        /// </summary>
        /// <remarks>
        /// If no input action has been assigned, the component remains inactive.
        /// </remarks>
        private void OnEnable()
        {
            if (backAction == null)
            {
                return;
            }

            backAction.action.performed += OnBackPerformed;
        }

        /// <summary>
        /// Unsubscribes from the configured input action when the component becomes inactive.
        /// </summary>
        private void OnDisable()
        {
            if (backAction == null)
            {
                return;
            }

            backAction.action.performed -= OnBackPerformed;
        }

        #endregion
        
        
        #region Input Handling
        
        /// <summary>
        /// Handles performed input actions and raises a back navigation request.
        /// </summary>
        /// <param name="context">
        /// Input System callback context associated with the performed action.
        /// </param>
        private void OnBackPerformed(InputAction.CallbackContext context)
        {
            RaiseBackRequested();
        }
        
        #endregion
    }
```

**How It Works**

The Unity Input System invokes a callback whenever the assigned Input Action is performed.

`InputSystemBackInputSource` responds by calling `RaiseBackRequested()`. From that point on, the request follows the same navigation pipeline as every other `BackInputSource` implementation.

```text
Input Action
      ↓
InputSystemBackInputSource
      ↓
RaiseBackRequested()
      ↓
BackRequested Event
      ↓
Navigation Controller
      ↓
Trailback
```

Like every `BackInputSource`, this component has a single job: detect back input and notify the rest of the navigation system.

It doesn't execute navigation, manage navigation history, or control your UI.

Because both input implementations raise the same `BackRequested` event, you can switch between the Legacy Input Manager and the Unity Input System without changing your Navigation Controller or Trailback integration.

**Configuration**

To use `InputSystemBackInputSource`:

1. Add the component to a GameObject in your scene.
2. Create an **Input Actions** asset if your project doesn't already have one.
3. Add a **Button** action for Back navigation.
4. Assign the Input Action to the **Back Action** field on `InputSystemBackInputSource`.
5. Assign the component to your Navigation Controller.
6. Subscribe to the `BackRequested` event, or connect it through the Inspector as shown in the README.

Once everything is connected, performing the assigned Input Action raises a `BackRequested` event.

Your Navigation Controller receives the event and forwards it to Trailback, where the normal navigation pipeline takes over.

The **Trailback UGUI Demo** includes a complete working implementation of this integration and an editor utility for switching between the Legacy Input Manager and the Unity Input System.

**Creating the Input Action**

If you're integrating the Unity Input System for the first time, create a Back Input Action using the following steps:

1. Create an **Input Actions** asset.
2. Add an **Action Map** (for example, **UI**).
3. Create a **Button** action named **Back**.
4. Add one or more bindings, such as:
    - **Keyboard** → Escape
    - **Android** → Back
    - **Gamepad** → Start or B (depending on your project)
5. Save the asset.
6. Drag the **Back** action into the **Back Action** field on `InputSystemBackInputSource`.

The specific binding doesn't matter. As long as the assigned Input Action is performed, `InputSystemBackInputSource` raises the `BackRequested` event.

From that point onward, Trailback processes the request exactly the same way as every other supported input implementation.

> [!NOTE]
>
> The Unity Input System integration is provided as a reference sample rather than part of Trailback's core framework. If the Input System package isn't installed, this sample won't be available.

> [!TIP]
>
> The **Trailback UGUI Demo** includes a small editor utility that lets you switch between the Legacy Input and Unity Input System implementations without manually changing the scene.

---

# Runtime Monitor

**Overview**

The Runtime Monitor lets you inspect Trailback's navigation state while your application is running.

It's included with the **Trailback UGUI Demo** and is useful when you're debugging navigation issues or simply want to see how Trailback responds as users move through your UI.

The Runtime Monitor is provided as a reference implementation, so you're free to use it as-is or adapt it to fit your own debugging workflow.

**Requirements**

Before using the Runtime Monitor, make sure:
* The **Trailback UGUI Demo** sample has been imported.
* The **Trailback Runtime Monitor** prefab has been added to the scene.
* The scene contains a **Canvas**.
* The scene contains an **Event System**.

> [!NOTE]
>
> The Runtime Monitor uses Unity UI. If your scene doesn't contain a Canvas or an Event System, the monitor won't function correctly until they're added.

**Setup**

The Runtime Monitor is included with the **Trailback UGUI Demo**.

To use it in your own project:

1. Import the **Trailback UGUI Demo** sample.
2. Locate the **Trailback Runtime Monitor** prefab.
3. Drag the prefab into your scene.
4. Press **Play** to begin monitoring Trailback's navigation state.

---

## What It Displays

The Runtime Monitor updates automatically while your application is running and displays information such as:

* Current navigation entry
* Navigation history
* Active navigation category
* Navigation depth
* Runtime statistics

Watching these values update in real time can make it much easier to understand how Trailback is resolving navigation requests and maintaining history.

## Customization

The Runtime Monitor is designed to be a starting point rather than a finished debugging tool.

You can customize it to match your own workflow by:

* Updating the user interface
* Displaying additional runtime information
* Adding your own statistics
* Integrating it into existing debugging tools

Many projects will find the included monitor sufficient, while others may choose to extend it with project-specific diagnostics or editor tooling.

The Runtime Monitor is intended as a learning and debugging tool rather than a production UI component.

> [!WARNING]
>
> The Runtime Monitor is distributed with the **Trailback UGUI Demo**. If you remove the sample from your project, the Runtime Monitor prefab and its supporting scripts will be removed as well. Existing Runtime Monitor instances may become missing or lose their script references.
>
> If you plan to customize or keep using the Runtime Monitor, it's a good idea to create your own copy inside your project's **Assets** folder before removing the sample.

---

## Next Steps

Once you've explored the demo, continue with the reference sample that best matches your project.

* **Legacy Input** — Integrate Trailback with Unity's Legacy Input Manager.
* **Unity Input System** — Integrate Trailback with the Unity Input System package.
* **Runtime Monitor** — Inspect and debug Trailback's navigation state while your application is running.
