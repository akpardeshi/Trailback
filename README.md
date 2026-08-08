# Trailback

> [!WARNING]
>
> ## Trailback v1.1.0-alpha
>
> This is the first public alpha release of Trailback.
>
> The core framework is feature complete, but it's still being tested across different Unity versions, project setups, and real-world use cases before the first stable release.
>
> **Current status**
>
> * ✅ Core framework is complete
> * ✅ Public API is largely stable
> * ✅ Reference samples are included
> * 🚧 Compatibility testing and community feedback are ongoing
>
> Although Trailback has been tested in a range of navigation scenarios, you may still run into bugs or edge cases.
>
> If you do, or if you have ideas for improving the framework, I'd really appreciate hearing from you. Please open an issue on GitHub:
>
> https://github.com/akpardeshi/Trailback/issues
>
> Thanks for taking the time to try Trailback and help shape the first stable release.


A lightweight back navigation framework for Unity that manages navigation across screens and popups using navigation history, categories, priorities, blockers, and root protection.

---

## Table of Contents

1. [What Is Trailback?](#what-is-trailback)
2. [Why I Built It](#why-i-built-it)

### Installation

3. [Installing Trailback](#installing-trailback)
   * [Requirements](#requirements)
   * [Step 1: Install via Git URL](#step-1--install-via-git-url)
   * [Step 2: Import Reference Samples](#step-2--import-reference-samples-optional)
   * [Step 3: Open Simplified Trailback UGUI Demo](#step-3--open-simplified-trailback-ugui-demo)

### Getting Started

4. [Understanding the Navigation Flow](#understanding-the-navigation-flow)

### Quick Start — Build Your First Integration

5. [Build Your First Trailback Integration](#quick-start---build-your-first-trailback-integration)
   * [Step 1: Create the Application Structure](#step-1--create-the-application-structure)
   * [Step 2: Create Navigation Categories](#step-2--create-navigation-categories)
   * [Step 3: Create UI Structure](#step-3--create-ui-structure)
   * [Step 4: Create a Reusable Base UI Class](#step-4--create-a-reusable-base-ui-class)
   * [Step 5: Create Screens](#step-5--create-screens)
   * [Step 6: Create Popups](#step-6--create-popups)
   * [Step 7: Create a Navigation Handler](#step-7--create-a-navigation-handler)
   * [Step 8: Create a Navigation Controller](#step-8--create-a-navigation-controller)
   * [Step 9: Connect Back Input](#step-9--connect-back-input)

6. [Integrating Trailback into an Existing Project](#integrating-trailback-into-an-existing-project)

### Documentations

7. [Documentation](#documentation)

---

## What Is Trailback?

Back navigation usually starts with something simple:

```csharp
Back();
```

That works well—until your UI grows beyond a few screens.

Once popups, overlays, and multiple navigation layers enter the picture, back navigation often becomes scattered across the project.

You may find yourself handling questions like:

* Should the popup close before the current screen?
* Which UI layer should respond to Back?
* Should Back be ignored while a confirmation dialog is open?
* How do you keep navigation consistent across different platforms?
* Why did Back return to the wrong screen?

Trailback is a lightweight Unity framework built specifically to solve those problems.

It manages navigation history, resolves which UI element should respond to Back, and lets your application decide how that navigation is performed. Trailback doesn't manage your UI, it simply determines where Back should go next.

```text
Back Requested
        ↓
Navigation History
        ↓
Navigation Categories
        ↓
Navigation Blocking
        ↓
Navigation Decision
        ↓
Your Application
```

### Core Features

| Feature                                                                                           | Description                                                                                                                                                           |
|---------------------------------------------------------------------------------------------------|-----------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| [**Navigation History**](Documentation/Guides/Features.md#navigation-history)                     | Keeps track of screens and popups, making it easy to return to previously visited UI.                                                                                 |
| [**Navigation Categories**](Documentation/Guides/Features.md#navigation-categories)               | Organize different UI layers and control which one responds first to Back navigation.                                                                                 |
| [**Navigation Blocking**](Documentation/Guides/Features.md#navigation-blocking)                   | Temporarily disable Back navigation while users complete a required workflow.                                                                                         |
| [**Root Protection**](Documentation/Guides/Features.md#root-protection)                           | Keep the root of your navigation flow in place instead of navigating past it.                                                                                         |
| [**OnNavigationRootReached Event**](Documentation/Guides/Features.md#onnavigationrootreached)                        | Notify your application when the user reaches the beginning of the navigation history.                                                                                |
| [**Input Abstraction**](Documentation/Guides/Features.md#input-abstraction)                       | Use the input system that already fits your project through `BackInputSource`.                                                                                        |
| [**Bridge-Based Integration**](Documentation/Guides/Features.md#bridge-based-integration)         | Keep Trailback isolated behind a dedicated integration layer instead of calling the framework throughout your application.                                            |
| [**Resetting Navigation History**](Documentation/Guides/Features.md#resetting-navigation-history) | Reset and rebuild navigation history whenever your application starts a new navigation flow, including scene reloads, level transitions, or returning to a main menu. |
| [**Trailback Debugger**](Documentation/Guides/Features.md#trailback-debugger)                     | `Trailback Debugger` allows the develoepr to debug `Trailback` navigation history.                                                                                                                                                                      |
| [**UI Framework Agnostic**](Documentation/Guides/Features.md#ui-framework-agnostic)               | Trailback manages navigation state rather than UI rendering, allowing it to integrate with UGUI, UI Toolkit, and custom UI frameworks without imposing a specific UI architecture.                                                                                                                                                                     |

If you'd like a closer look at how these features work, continue with the [**Features Guide**](Documentation/Guides/Features.md).

---


## Why I Built It

Most back button implementations work well at the beginning of a project.

Over time they usually evolve into a collection of special cases:

* Close popup before screen
* Prevent accidental navigation
* Handle Android back button
* Support multiple UI layers
* Debug unexpected navigation behavior

Trailback is a small, focused framework that solves these problems without requiring a specific UI framework or project architecture.

---


## Installing Trailback

### Requirements

> [!IMPORTANT]
>
> Trailback is developed and tested with **UGUI**.
>
> While the core framework isn't tied to a specific UI system, the included **Trailback UGUI Demo**, **Simplified Trailback UGUI Demo** and **Runtime Monitor** reference are currently built with UGUI.
> 
> Before importing the **Trailback UGUI Demo**, make sure the **TextMeshPro** and **Input System** packages are installed if you plan to use the included reference samples. This avoids compilation errors caused by missing package dependencies.

Trailback has **no required package dependencies**.

Some reference samples may require additional Unity packages depending on what they demonstrate. Refer to the [**Reference Samples Guide**](Documentation/Guides/ReferenceSamplesGuide.md) for sample-specific requirements, setup instructions, and supported workflows.

---

Trailback is distributed as a Unity Package Manager (UPM) package.

### Step 1 — Install Via Git URL

Open:

```text
Window
    → Package Manager
```

Click:

```text
+
    → Add Package From Git URL...
```

Enter:

```text
https://github.com/akpardeshi/Trailback.git
```

https://github.com/user-attachments/assets/86313e72-b50a-4d10-91c5-d15b6e74d294

Unity will download and install Trailback automatically.

---

### Step 2 — Import Reference Samples (Optional)

After installation, select the Trailback package inside Package Manager.

Open the **Samples** section.

Import:

```text
Trailback UGUI Demo
```

The package includes two demo scenes: Trailback UGUI Demo(Demonstrates all Trailback features working together) and Simplified Trailback UGUI Demo(Used throughout the Quick Start). Throughout the Quick Start, you'll build the same navigation flow used in the Simplified Trailback UGUI Demo.

https://github.com/user-attachments/assets/1837d456-d5ea-4edc-988c-367dd89d9195

> [!NOTE]
>
> Reference samples are optional.
>
> You can use Trailback without importing any of them. Simply import the samples that match the features or workflows you want to explore.

---


### Step 3 — Open Simplified Trailback UGUI Demo

After importing the sample:

```text
Assets
    → Samples
        → Trailback
            → [1.1.0-alpha]
                → Simplified Trailback UGUI Demo
```

<img width="397" height="282" alt="OpenSampleProject" src="https://github.com/user-attachments/assets/547393e3-8aa0-4e66-ab96-f670a8c81400" />

Open the **Simplified Trailback UGUI Demo** and press **Play**.

The Simplified Trailback UGUI Demo demonstrates Trailback's fundamental features working together in a single scene.

---


## Understanding The Navigation Flow

Before configuring Trailback, it helps to understand how a back navigation request travels through the framework.

```text
User Presses Back
        ↓
BackInputSource
        ↓
Navigation Controller
        ↓
TrailbackFacade
        ↓
Navigation Handler
        ↓
Hide Current UI / Show Previous UI
```

Trailback itself never reads keyboard, gamepad, touch, or mobile input directly. Input is always supplied through a `BackInputSource`.

> [!TIP]
>
> The **Simplified Trailback UGUI Demo** is the reference project used throughout the documentation.
>
> If you're following the [**Quick Start**](#quick-start--build-your-first-integration) or [**Troubleshooting**](Documentation/Guides/Troubleshooting.md) guide and something isn't working as expected, compare your project with the **Simplified Trailback UGUI Demo**. It's the easiest way to verify your setup and spot any missing configuration.

---


## Quick Start - Build Your First Trailback Integration

This guide takes you through the process of integrating Trailback into a project from the ground up.

By the end, you'll have a working navigation setup with screens, popups, and back input fully connected and ready to build on.

```text
2 Screens
1 Popup
Back Input
```

### Step 1 — Create the Application Structure

Before adding Trailback, create the objects that will handle navigation in your scene.

Your hierarchy should look like this:

```text
Scene
├── Navigation Controller
├── Back Input Source
```

The Navigation Controller and `BackInputSource` are the only Trailback-specific objects required to get started. The rest of your UI remains part of your application.


#### Navigation Controller

Create an empty GameObject named **Navigation Controller**.

This object acts as the entry point for your application's navigation. It will:

* Receive requests.
* Report navigation changes to Trailback.
* Register the application's navigation handler.
* Coordinate navigation between your application and Trailback.

Later in this guide, you'll attach the `SampleNavigationController` component to this GameObject.


#### `BackInputSource`

Create a GameObject with the `BackInputSource` implementation you want to use.

Trailback doesn't depend on a specific input system, so you can choose the approach that matches your project.

The **Reference Samples Guide** includes complete examples for:

* [Legacy Input](Documentation/Guides/ReferenceSamplesGuide.md#legacy-input)
* [Unity Input System](Documentation/Guides/ReferenceSamplesGuide.md#unity-input-system)


### Step 2 — Create Navigation Categories

The **Navigation Categories** are used to prioritize and group the UI elements.

For this guide, create the two **Navigation Category** ScriptableObjects as shown below:

```text
Create
    → Trailback
        → Navigation
            → Navigation Category
```

<img width="1088" height="737" alt="Creating Navigation Categories" src="https://github.com/user-attachments/assets/91c9cadb-bb99-4c85-a2b0-6f357571584a" />

Apply this settings to the new **Navigation Category** ScriptableObjects:

**Screen Category:**

| Setting | Value |
|---|----------|
| Priority | 0 |
| Protect Root Element | **True** |
| Duplicate Policy | Allow    |

**Popup Category:**

| Setting | Value |
|---|---|
| Priority | 100 |
| Protect Root Element | False |
| Duplicate Policy | Ignore |

<img width="539" height="350" alt="Root Protection" src="https://github.com/user-attachments/assets/bfc7c401-a990-4e11-b294-02726e790444" />

> [!IMPORTANT]
>
> In most projects, only **one** navigation category should have **Protect Root Element** enabled.
>
> To learn how Navigation Categories, Root Protection, Priorities, and Duplicate Policy work, see the [**Trailback Developer Guide**](Documentation/Guides/TrailbackDeveloperGuide.md#root-protection).


### Step 3 — Create UI Structure

In your scene, create:

1. A Canvas (if you don't have one)
2. Under the Canvas, create a Panel for each screen/popup:
   * HomeScreen
   * AboutScreen 
   * InfoPopup 
3. Add a Buttons to HomeScreen: `Open About` → wire to `SampleNavigationController.ShowAboutScreen()` and `Open Info` → `SampleNavigationController.ShowInfoPopup()`   
4. Add a Buttons to AboutScreen: `Show Info` → wire to `SampleNavigationController.ShowInfoPopup()`, and `Home` → `SampleNavigationController.OpenRootScreen()`.   
5. Add a Button to InfoPopup: `Close` → wire to `SampleNavigationController.HideInfoPopup()`, and `Home` → `SampleNavigationController.OpenRootScreen()`.

Each panel needs:
* Canvas component
* CanvasGroup component
* GraphicRaycaster
* CanvasScaler
* The corresponding UI subclass (HomeScreen, AboutScreen, InfoPopup)


### Step 4 — Create A Reusable Base UI Class

Create a reusable `UIBase` class that implements `IBackNavigable`. Screens and popups created later in this guide will inherit from this class.

Classes must implement the `IBackNavigable` interface to participate in Trailback's navigation history.

```csharp
    using UnityEngine;
    using ModularForge.Trailback.Core;

    [RequireComponent(typeof(Canvas))]
    [RequireComponent(typeof(CanvasGroup))]
    public abstract class UIBase : MonoBehaviour, IBackNavigable
    {
        private Canvas _canvas;
        private CanvasGroup _canvasGroup;

        [field: SerializeField] public NavigationCategorySo NavigationCategory { get; private set; }

        protected virtual void Awake()
        {
            _canvas = GetComponent<Canvas>();
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        /// <summary>
        /// Makes the UI element visible.
        ///
        /// This method manages the visibility lifecycle and invokes <see cref="OnShown"/>
        /// after the element successfully becomes visible.
        /// </summary>
        public void Show()
        {
            if (IsVisible())
            {
                return;
            }

            ManageCanvas(true);
            ManageCanvasGroup(true);

            OnShown();
        }

        /// <summary>
        /// Hides the UI element.
        ///
        /// This method manages the visibility lifecycle and invokes <see cref="OnHidden"/>
        /// after the element successfully becomes hidden.
        /// </summary>
        public void Hide()
        {
            if (!IsVisible())
            {
                return;
            }

            ManageCanvas(false);
            ManageCanvasGroup(false);

            OnHidden();
        }

        /// <summary>
        /// Called after the UI element successfully becomes visible.
        /// Override this method to perform initialization or other logic
        /// that should run after the element is shown.
        /// </summary>
        protected virtual void OnShown()
        {
        }

        /// <summary>
        /// Called after the UI element successfully becomes hidden.
        /// Override this method to perform cleanup or other logic
        /// that should run after the element is hidden.
        /// </summary>
        protected virtual void OnHidden()
        {
        }
        
        
        private bool IsVisible()
        {
            return _canvas.enabled; 
        }
        
        private void ManageCanvasGroup(bool isActive)
        {
            _canvas.enabled = isActive;
        }

        private void ManageCanvas(bool isActive)
        {
            _canvasGroup.alpha = isActive ? 1 : 0;
            _canvasGroup.blocksRaycasts = isActive;
            _canvasGroup.interactable = isActive;
        }
    }
```

> [!Note]
>
> `UIBase` provides a reusable implementation shared by screens, and popups. This version is intentionally simplified for the Quick Start.
>
> Override `OnShown()` and `OnHidden()` to implement custom behavior when the UI becomes visible or hidden. Do not override `Show()` and `Hide()`.
>
> To learn more about `IBackNavigable`, see the [**Trailback Developer Guide**](Documentation/Guides/TrailbackDeveloperGuide.md).
>
> The **FeaturesScreen** class in the **Trailback UGUI Demo** demonstrates how to use the `OnShown()` and `OnHidden()` lifecycle callbacks.

Every subclass must have its `NavigationCategory` assigned through the Inspector.


### Step 5 — Create Screens

Create two new scripts that inherit from `UIBase`. Name them **HomeScreen** and **AboutScreen**.

**Home Screen**

```csharp
/// <summary>
/// Represents the application's home screen.
/// </summary>
public class HomeScreen : UIBase 
{
     // Home Screen specific behavior would typically live here.
}
```

**About Screen**

```csharp
/// <summary>
/// Represents the application's About screen.
/// </summary>
public class AboutScreen : UIBase 
{
    // About Screen specific behavior would typically live here.
}
```

Assign the **Screen Category** to both screens in the Inspector.


### Step 6 — Create Popups

Create a new script that inherit from `UIBase`. Name it **InfoPopup**.

**InfoPopup**:

```csharp
 public class InfoPopup : UIBase
{
    // Info Popup specific behavior would typically live here.
}
```

Assign the **Popup Category** to the `InfoPopup` in the Inspector.


### Step 7 — Create A Navigation Handler

Trailback is responsible for deciding **which** UI element should be shown next, but it doesn't directly show or hide your UI. Instead, it delegates that responsibility to an `IBackNavigationHandler`.

```text
User Presses Back
        ↓
TrailbackFacade.Back()
        ↓
Navigation Handler
        ↓
Hide Current Screen
Show Previous Screen
```

Create the following navigation handler:

```csharp
    using ModularForge.Trailback.Core;

    public class DemoBackNavigationHandler : IBackNavigationHandler
    {
        public void NavigateBackTo(BackContext context)
        {
            if (context.Current is UIBase currentUI)
            {
                currentUI.Hide();
            }

            if (context.BackTarget is UIBase backTargetUI)
            {
                backTargetUI.Show();
            }
        }
    }
```

> [!Important]
>
> The UI implementation should ensure that the `Show()` and `Hide()` methods safely ignore redundant visibility requests. This keeps the navigation handler focused on navigation while allowing each UI implementation to decide how visibility is managed.
>
> **Avoid directly enabling and disabling GameObjects inside navigation handlers.** Prefer a UI abstraction such as `UIBase.Show()` / `UIBase.Hide()`, as shown above. This keeps navigation behavior separate from UI implementation details.

To learn more about `IBackNavigationHandler` refer [**Trailback Developer Guide**](Documentation/Guides/TrailbackDeveloperGuide.md#navigation-handler)

### Step 8 — Create a Navigation Controller

The Navigation Controller coordinates your application's back navigation.

It coordinates your application's navigation, reports navigation changes to `TrailbackFacade`, and responds to navigation requests.

In most projects, this becomes the central place where your application's navigation logic and Trailback come together.

Create a new unity script and name it **SampleNavigationController:**

```csharp
    using ModularForge.Trailback.Core;
    using UnityEngine;

    /// <summary>
    /// Simple navigation controller used by the Quick Start sample.
    ///
    /// This implementation keeps the navigation flow explicit and easy to follow.
    /// For larger projects, see the Complete UGUI Demo and the Trailback Developer Guide.
    /// </summary>
    public class SampleNavigationController : MonoBehaviour
    {
        [SerializeField] private HomeScreen homeScreen;
        [SerializeField] private AboutScreen aboutScreen;

        [SerializeField] private InfoPopup infoPopup;
        
        private void Awake()
        {
            // Reset the navigation history to start new session
            TrailbackFacade.ResetHistory();

            TrailbackFacade.SetNavigationHandler(new DemoBackNavigationHandler());
        }
        
        private void Start()
        {
            homeScreen.Hide();
            aboutScreen.Hide();
            infoPopup.Hide();

            ShowHomeScreen();
        }

        public void ShowHomeScreen()
        {
            homeScreen.Show();
            TrailbackFacade.ReportShown(homeScreen);
        }

        private void HideHomeScreen()
        {
            // IMPORTANT: Do NOT call TrailbackFacade.ReportHidden(homeScreen) here.
            // To keep Home in history so pressing Back returns to it.
            // Only call TrailbackFacade.ReportHidden(homeScreen); if you want to completely remove 
            // an element from history.

            homeScreen.Hide();
        }

        public void ShowAboutScreen()
        {
            HideHomeScreen();

            aboutScreen.Show();
            TrailbackFacade.ReportShown(aboutScreen);
        }

        public void ShowInfoPopup()
        {
            infoPopup.Show();
            TrailbackFacade.ReportShown(infoPopup);
        }

        public void HideInfoPopup()
        {
            infoPopup.Hide();
            TrailbackFacade.ReportHidden(infoPopup);
        }

        public void OpenRootScreen()
        {
            aboutScreen.Hide();
            infoPopup.Hide();

            // Clear the history when opening Root Screen
            // In this demo HomeScreen is the Root Screen 
            TrailbackFacade.ResetHistory();

            ShowHomeScreen();
        }
    }
```

The `OpenRootScreen()` method hides the **AboutScreen** and **InfoPopup**. Before showing the **HomeScreen**, it resets the navigation history.

Opening the root screen can be performed from anywhere in your application. Unlike back navigation, opening the root screen does not follow the existing navigation history. For this reason, you should reset the navigation history before showing the root screen. Trailback provides `TrailbackFacade.ResetHistory()` to reset the navigation history.

> [!NOTE]
> 
> This example focuses only on the Trailback integration to keep the code easy to follow. The **Trailback UGUI Demo** also includes application-specific code for screen management, popup management, lookup tables, and initialization, but those parts have been left out to keep the example focused.


### Step 9 — Connect Back Input

In [**Step 8**](#step-8--create-a-navigation-controller), you created `SampleNavigationController`. Now it's time to connect a BackInputSource so it can receive back navigation requests.

`BackInputSource` is how Trailback receives back navigation requests. It works with Unity's Legacy Input Manager, the Unity Input System, or any custom input solution that raises the `BackRequested` event.

This example uses the **Unity Input System**. Create a new class named `InputSystemBackInputSource` that inherits from `BackInputSource`.

```csharp
    using ModularForge.Trailback.Core;
    using UnityEngine;
    using UnityEngine.InputSystem;
    
    public class InputSystemBackInputSource : BackInputSource
    {
        #region Configuration

        [SerializeField]
        private InputActionReference backAction;
        
        #endregion
        
        
        #region Unity Lifecycle
        
        private void OnEnable()
        {
            if (backAction == null)
            {
                return;
            }

            backAction.action.performed += OnBackPerformed;
        }
        
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
        
        private void OnBackPerformed(InputAction.CallbackContext context)
        {
            RaiseBackRequested();
        }
        
        #endregion
    }
```

Create the new GameObject in scene with name `InputSource`, and attach the script `InputSystemBackInputSource` on that GameObject. Finally assign the proper input action on `InputSystemBackInputSource`. 

Inside `SampleNavigationController` add the variable to store `BackInputSource`,

```csharp
[SerializeField] private BackInputSource backInputSource;
```

Copy the method `CacheComponents()` shown below inside `SampleNavigationController`, and call it from the `Awake()` method.

```csharp
private void CacheComponents()
{
    if (backInputSource)
    {
        return;
    }   

    backInputSource = FindAnyObjectByType<BackInputSource>();
}
```

This method automatically finds a `BackInputSource` in the scene if one has not been assigned in the Inspector.

Copy and paste the `OnEnable()` and `OnDisable()` methods shown below. This methods will subscribe and unsubscribe to the `BackRequested` event of `BackInputSource`. The event `BackRequested` is raised when a back input is detected.

```csharp
private void OnEnable()
{
    if (backInputSource == null) return;

    backInputSource.BackRequested += HandleBackRequested;
}

private void OnDisable()
{
    if (backInputSource == null) return;

    backInputSource.BackRequested -= HandleBackRequested;
}
```

Finally, copy and paste the method `HandleBackRequested()`, this method will call the `TrailbackFacade.Back()` method. This method forwards the back request to `TrailbackFacade`, allowing Trailback to execute back navigation.  

```csharp
private void HandleBackRequested()
{
    TrailbackFacade.Back();
}
```

To learn more about input support for Trailback refer to the [**Reference Samples Guide**](Documentation/Guides/ReferenceSamplesGuide.md#included-content) includes complete examples for both supported Unity input systems.


## Integrating Trailback into an Existing Project

If your project already includes screens, popups, and a navigation system, then there is no need for you to redo your UI. Instead, you should integrate **Trailback** into the navigation process that you currently have.

At a minimum, your existing project should:

* Ensure that each screen and each popup implements `IBackNavigable`.
* Report navigation changes using `TrailbackFacade.ReportShown()` and `TrailbackFacade.ReportHidden()`.
* Register a navigation handler with `TrailbackFacade.SetNavigationHandler()`.
* When your application receives a request for back navigation, call `TrailbackFacade.Back()`.

The steps mentioned in the Quick Start remain unchanged; you just need to substitute your current UI and navigation system for the sample scripts.

> [!TIP]
>
> The **Trailback Developer Guide** gives a detailed explanation of each feature and also includes further integration examples.


### Checkpoint — Verify navigation history

Verify the following navigation scenarios before continuing:
1. On the HomeScreen, click the About button. Press Back. The AboutScreen should be hidden, and the HomeScreen should become visible.
2. On the HomeScreen, open the InfoPopup. Press Back. The InfoPopup should be hidden.
3. On the HomeScreen, click the About button, then click the InfoPopup button.

   Your navigation history should now be:

   ```text
   HomeScreen → AboutScreen → InfoPopup
   ```

4. Click the Close button on the InfoPopup. The popup should close.
5. Open the InfoPopup again. Press Back. The InfoPopup should close. Press Back again. The AboutScreen should be hidden, and the HomeScreen should become visible.
6. Open AboutScreen, then click Home. Press Back. The AboutScreen should not become visible because the navigation history was reset.


### Congratulations!

If everything worked as expected, congratulations! You have successfully integrated Trailback into your project.

Continue to the [**Documentation**](#documentation) section below to explore the rest of the Trailback guides and learn about the available features.

If you encounter any errors or unexpected behavior, go through **Steps 1–9** of this [**Quick Start**](#quick-start--build-your-first-integration) again or compare your project with the **Simplified Trailback UGUI Demo** included with Trailback.

---

### Debugging
To help developers debug the navigation history, Trailback provides the **Trailback Debugger** tool. It can be opened through 

```text
Tools 
    → Trailback  
        → Trailback Debugger.
```

<img width="880" height="70" alt="Open Trailback Debugger" src="https://github.com/user-attachments/assets/9585892e-9d56-4409-8a5b-5471d8f96765" />


[Click here to learn more about the **Trailback Debugger**](Documentation/Guides/Features.md#trailback-runtime-debugger).

---


## Documentation

Finished the Quick Start? Continue with the guide that best matches what you'd like to explore next.

### Getting Started

* 📖 **[README](#table-of-contents)**
  * Return to the main documentation, installation instructions, and Quick Start guide.

### Learn the Framework

* ✨ **[Features Guide](Documentation/Guides/Features.md)**
  * Learn how Trailback works, from navigation history and categories to root protection and navigation blockers.

### Reference Samples

* 🧩 **[Reference Samples Guide](Documentation/Guides/ReferenceSamplesGuide.md)**
  * Explore the **Trailback UGUI Demo** along with the Legacy Input, Unity Input System, and Runtime Monitor samples.

### Integration

* 🔄 **[Handling Scene Changes](Documentation/Guides/HandlingSceneChanges.md)**
  * Learn how to manage navigation history across scene reloads, and scene transitions.

### Troubleshooting

* 🛠️ **[Troubleshooting Guide](Documentation/Guides/Troubleshooting.md)**
  * Resolve common setup and integration issues, and verify that your Trailback configuration is working correctly.

### Trailback Developer Guide
* 📖 **[Trailback Developer Guide](Documentation/Guides/TrailbackDeveloperGuide.md)**
  * Learn how to integrate Trailback features into your project with practical examples, implementation guides, and best practices.
