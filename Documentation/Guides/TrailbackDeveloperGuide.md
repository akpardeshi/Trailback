# Table of Contents

* [Introduction](#introduction)

* [Part 1 – Trailback UGUI Demo](#part-1)
    * [Opening the Demo](#opening-the-demo)
    * [Trailback UGUI Demo](#trailback-ugui-demo)
    * [Core Features](#core-features)
    * [Included Reference Implementations](#included-reference-implementations)
    * [What You'll See](#what-youll-see)
    * [What You'll Learn From the Demo](#what-youll-learn-from-the-demo)
    * [Recommended Learning Order](#recommended-learning-order)

* [Part 2 – Feature Integration](#part-2)
    * [Navigation Category](#navigation-category)
        * [Duplicate Policy](#duplicate-policy)
        * [Root Protection](#root-protection)
    * [Navigation Blocking](#navigation-blocking)
    * [IBackNavigable](#ibacknavigable)
    * [Navigation Handler](#navigation-handler)
    * [Navigation Bridge](#navigation-bridge)
    * [OnNavigationRootReached Event](#onnavigationrootreached-event)
    * [Input Integration](#input-integration)
    * [Resetting Navigation History](#resetting-navigation-history)

* [Next Steps](#next-steps)


## Introduction
The purpose of this guide is to teach developers how to integrate Trailback features. Developers should read this guide after completing the Quick Start in [README.md](../../README.md#table-of-contents) and setting up Trailback in their scene. This guide is divided into two parts. The first part explains the **Trailback UGUI Demo** included with Trailback, and the second part covers how to integrate all Trailback features. Each section in Part 2 is self-contained, allowing developers who have completed the Quick Start to read, understand, and implement a feature without relying on any other section of this guide.


## Part 1
Part 1 of this guide provides a brief tour of the **Trailback UGUI Demo**, where you can explore all of Trailback's core features working together in a single scene.

### Opening the Demo

After importing the sample:

```text
Assets
    → Samples
        → Trailback
            → [1.1.0-alpha]
                → Trailback UGUI Demo
```

Open the **Trailback UGUI Demo** and press **Play**.

---

### Trailback UGUI Demo

The package includes the **Trailback UGUI Demo**, a fully working project that brings Trailback's core features together in a realistic navigation flow.

Many of the examples throughout the documentation are based on this project, so it's a great place to explore the framework, experiment with different scenarios, and compare your own integration against a working example.

The demo serves as both:

* **Feature Showcase**
* **Reference Implementation**
* **Integration Reference**

and is the fastest way to learn how Trailback is intended to be configured and integrated.

https://github.com/user-attachments/assets/a1d0ad52-c798-4c95-98a0-bc0ae1ed2504

### Core Features
* [Navigation History](Features.md#navigation-history)
* [Navigation Categories](Features.md#navigation-categories)
* [Navigation Blocking](Features.md#navigation-blocking)
* [Root Protection](Features.md#root-protection)
* [OnNavigationRootReached](Features.md#onnavigationrootreached)
* [Input Abstraction](Features.md#input-abstraction)
* [Bridge-Based Integration](Features.md#bridge-based-integration)
* [Scene Reload Support](Features.md#scene-reload-support)
* [UI Framework Agnostic](Features.md#ui-framework-agnostic)
* [Trailback Debugger](Features.md#trailback-debugger)

### Included Reference Implementations
* [Legacy Input](ReferenceSamplesGuide.md#legacy-input)
* [Unity Input System](ReferenceSamplesGuide.md#unity-input-system)
* [Runtime Monitor](ReferenceSamplesGuide.md#runtime-monitor)

### What You'll See

The demo includes working examples of:
* Category priority
* Layered navigation
* Popup resolution
* Back Input Sources


This makes it easy to observe how Trailback responds to navigation requests in different situations and how multiple navigation features interact with one another.

### What You'll Learn From the Demo

The included scripts provide practical examples of:

* Implementing `IBackNavigable`
* Creating Navigation Categories
* Creating Navigation Handlers
* Creating a Trailback Integration Bridge
* Connecting Back Input Sources
* Implementing `IBackNavigationBlocker`
* Responding to `OnNavigationRootReached`

### Recommended Learning Order

```text
Import Trailback UGUI Demo
        ↓
Press Play
        ↓
Explore Core Navigation Features
        ↓
Review the Demo Scripts
        ↓
Build Your Own Integration
        ↓
Explore Additional Reference Samples
```

The demo is designed to be explored and adapted as you learn Trailback.

Looking for a specific sample?

The [**Reference Samples Guide**](ReferenceSamplesGuide.md) explains what each reference implementation demonstrates, when to use it, and how to set it up in your project.

---


## Part 2
The purpose of Part 2 is to teach developers how to integrate each Trailback feature individually. Each section is self-contained whenever possible, allowing developers to jump directly to a specific feature without reading the rest of the guide. You can open any feature section to learn how to understand and implement it in your project.

### Navigation Category

Navigation Categories define how Trailback groups and prioritizes navigation.

Every screen, or popup belongs to a navigation category. This allows different parts of your UI to maintain their own navigation history while giving Trailback enough information to decide which category should respond when **Back** is pressed.

For this guide, create the following two navigation categories:


```text
Create
    → Trailback
        → Navigation
            → Navigation Category
```

<img width="1088" height="737" alt="Creating Navigation Categories" src="https://github.com/user-attachments/assets/91c9cadb-bb99-4c85-a2b0-6f357571584a" />


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

Higher priority categories resolve first:

```text
Back Requested
      ↓
Popup Category (Priority 100)
      ↓
Screen Category (Priority 0)
```

These priorities are used throughout the **Quick Start**, **Trailback UGUI Demo** and **Simplified Trailback UGUI Demo**. Your own project can use different values depending on how you organize your navigation.

#### Duplicate Policy

The Navigation Category also contains the `DuplicatePolicy` setting. If the developer sets it to `Allow`, Trailback stores duplicate entries of the same navigation element in the navigation history. If the developer sets it to `Ignore`, Trailback ignores duplicate entries and keeps only the first occurrence of that navigation element in the history.

For Allow:

```text
Home
↓
Settings
↓
Home

History:
Home
Settings
Home
```

For Ignore:

```text
Home
↓
Settings
↓
Home

History:
Home
Settings
```

#### Root Protection
Root Protection is a sub-feature of Navigation Categories. In many projects, developers want to make sure that the Root Screen stays visible when there is no screen to navigate back to. To achieve this, enable Root Protection. When the user tries to navigate back from the Root Screen instead of closing the screen, the Trailback raises the `OnNavigationRootReached` event.  

In order to enable root protection on navigation category select the Navigation Category ScriptableObject and click on `Protect Root Element` and set it to `true`. 

`Protect Root Element = true` prevents the root navigation element from being removed from the navigation history. When Back is pressed at the beginning of the navigation flow, Trailback raises the **OnNavigationRootReached** event instead of attempting another back navigation.

The following diagram illustrates how Trailback behaves when Back is pressed while the Root Screen is active.

```text
Back Button
      ↓
TrailbackFacade.Back()
      ↓
Root Protected?
   ┌──────┴──────┐
   │             │
  Yes            No
   ↓             ↓
Raise            Remove Root
OnNavigation     From History
RootReached      ↓
Event            Navigate Back
```

<img width="539" height="350" alt="Root Protection" src="https://github.com/user-attachments/assets/bfc7c401-a990-4e11-b294-02726e790444" />

> [!IMPORTANT]
>
> In most projects, only **one** navigation category should have **Protect Root Element** enabled.
>
> This category acts as the root of your navigation flow. Enabling Root Protection on multiple categories can make it unclear which category should stop back navigation.
>
> To learn more about how **Root Protection** works and when to use it, see the **Features Guide** → [Root Protection](Features.md#root-protection).

---

### IBackNavigable

Trailback stores the navigation history as a collection of objects implementing the `IBackNavigable` interface. Every screen or popup that should participate in back navigation must implement this interface. If a class does not implement `IBackNavigable`, Trailback will ignore it and it will not be added to the navigation history.

The `IBackNavigable` interface only contains the `NavigationCategory` property. Trailback uses this property to group navigation elements and determine which category should respond when Back is pressed.

Every screen and popup used throughout this guide implements `IBackNavigable`.

```csharp
/// <summary>
/// Represents the application's home screen.
/// </summary>
public class HomeScreen : MonoBehaviour, IBackNavigable
{
    // Home Screen-specific behavior would typically live here.
}
```

> [!TIP]
>
> If a screen or popup is not participating in back navigation, first verify that it implements `IBackNavigable`. Then make sure it reports its visibility by calling `TrailbackFacade.ReportShown()` when it becomes visible and `TrailbackFacade.ReportHidden()` when it is hidden.

---


### Navigation Blocking

Navigation Blocking is useful when the user must complete an action before the element can be closed. There are multiple cases where the user must provide explicit choice like a purchase confirmation, an unsaved changes warning, or a terms acceptance screen. In such scenarios the screen, to close the screen the user must dismiss the screen by clicking on the button. 

In order to implement Navigation Blocking on a particular element, the class must implement the `IBackNavigationBlocker` interface and set `BackNavigationMode` to `BackNavigationMode.Block`. When the blocked element is at the top of the navigation history and the user tries to navigate back, Trailback ignores the back navigation request and returns without performing any navigation.

```csharp
public class PurchaseConfirmationPopup : UIBase, IBackNavigationBlocker
{
    [field: SerializeField]
    public BackNavigationMode BackNavigationMode { get; private set; } = BackNavigationMode.Block;
}
```

```text
Allow  → Navigation continues
Block  → Navigation stops
```

When a blocker is active:

```text
Back Button
        ↓
Blocked
```

https://github.com/user-attachments/assets/35a5a1f2-4278-47f4-916a-f4d71e386bc7

---


### Navigation Handler

Trailback handles the resolution of back navigation, but it does not itself show or hide UI elements; it instead passes the navigation operation on to an `IBackNavigationHandler`.

The `IBackNavigationHandler` functions as the point at which Trailback is integrated with the UI system that your project already has. Developers are required to implement this interface in order to link Trailback with their current screen, popup, or UI elements.

```text
Back Requested
        ↓
Trailback
        ↓
Resolve Navigation
        ↓
IBackNavigationHandler
        ↓
Your UI Implementation
```

The method `NavigateBackTo()` takes as its parameter a `BackContext` which includes the current navigation element as well as the navigation element that should become active after the back navigation has taken place.

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

> [!IMPORTANT]
>
> The `IBackNavigationHandler` carries out the navigation operation and does not check the current visibility state of your UI.

> The UI implementation must make sure that the `Show()` and `Hide()` methods are able to safely disregard any redundant visibility requests; this allows the navigation handler to remain focused on navigation while at the same time enabling each UI implementation to determine how visibility is managed.


### Navigation Bridge

The integration bridge acts as the connection between your application and Trailback.

Instead of calling the framework directly throughout your project, your application communicates with the bridge. The bridge then forwards those requests to Trailback, giving you a single place to manage the integration.

Keeping that boundary in one place makes your navigation code easier to maintain and gives you the flexibility to change or extend your implementation later if your project's architecture evolves.

Trailback provides the abstract class `TrailbackIntegrationBridge`. To implement a bridge, create a new class that inherits from `TrailbackIntegrationBridge`. 

The following implementation forwards every navigation request to `TrailbackFacade` while exposing a single integration point for the rest of the application.

```csharp
using System;
using ModularForge.Trailback.Core;

public class DemoTrailbackBridge : TrailbackIntegrationBridge
{
    public override void InitializeSession()
    {
        TrailbackFacade.ResetHistory();     
    }
    
    public override event Action OnNavigationRootReached
    {
        add => TrailbackFacade.OnNavigationRootReached += value;
        remove => TrailbackFacade.OnNavigationRootReached -= value;
    }

    public override void SetNavigationHandler(IBackNavigationHandler handler)
    {
        TrailbackFacade.SetNavigationHandler(handler);
    }
    
    public override void Show(IBackNavigable element) => TrailbackFacade.ReportShown(element);

    public override void Hide(IBackNavigable element) => TrailbackFacade.ReportHidden(element);

    public override bool Back() => TrailbackFacade.Back();

    public override void ResetHistory() => TrailbackFacade.ResetHistory();
}
```


### OnNavigationRootReached Event

When the user reaches the beginning of the navigation history and presses Back again, Trailback raises the **OnNavigationRootReached** event instead of attempting another back navigation. For the **OnNavigationRootReached** event to be raised, **Root Protection** must be enabled on the root Navigation Category. 

```text
Home Screen 
    ↓
Press Back 
    ↓ 
OnNavigationRootReached Event 
    ↓ 
Your Application Decides What Happens Next
```

At this point, Trailback stops processing the navigation request and hands control back to your application.

https://github.com/user-attachments/assets/63ad1f89-9119-4ed8-b920-3a870fac7504

Trailback provides three ways to respond when the navigation root is reached.

Choose how to respond using one of these approaches:

| Approach                          | Best For                                           |
|-----------------------------------| -------------------------------------------------- |
| **Subscribe Through a Bridge** | Production projects (Recommended)                  |
| **Trailback Event Listener**      | Designer-driven workflows                          |
| **Direct Event Subscription**     | Small projects, prototypes                         |

Common responses include:

* Showing a confirmation popup
* Returning to the main menu
* Exiting the application
* Recording analytics events
* Playing UI or audio feedback

> [!NOTE]
>
> The Trailback Event Listener is completely optional.
>
> If you're already handling the **OnNavigationRootReached** event through a `TrailbackIntegrationBridge` or `TrailbackFacade`, there's no need to add an Event Listener.


#### Example: Adding a Confirmation Popup

Create a `ConfirmationPopup` script:

```csharp
using ModularForge.Trailback.Core;
using UnityEngine;

public class ConfirmationPopup : UIBase
{
    public void Stay()
    {
        Hide();
        TrailbackFacade.ReportHidden(this);
    }

    public void Exit()
    {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            UnityEngine.Application.Quit();
#endif
    }
}
```

Create two buttons under your ConfirmationPopup. Wire them:

- **Stay button** → `ConfirmationPopup.Stay()`
- **Exit button** → `ConfirmationPopup.Exit()`

Add a reference to the confirmation popup in your `DemoNavigationController` or any equivalent script managing navigation:

```csharp
[SerializeField] private ConfirmationPopup confirmationPopup;
```

In the `Start()` method add the following line to hide the `ConfirmationPopup`

```csharp
confirmationPopup.Hide();
```

Then assign the **Confirmation Popup** GameObject to this field in the Inspector.

The following example demonstrates all three approaches using a `ConfirmationPopup`.

Choose the approaches that best fits your project.

#### Option A — Subscribe Through a Bridge (Recommended)

Inside your `DemoNavigationController` or any equivalent script managing navigation,

**Add ONE line to your existing `OnEnable()`, if there is no `OnEnable()` method then create new `OnEnable()` method:**

```csharp
_bridge.OnNavigationRootReached += HandleNavigationRootReached;
```

**Add ONE line to your existing `OnDisable()`, if there is no `OnDisable()` method then create new `OnDisable()` method:**

```csharp
_bridge.OnNavigationRootReached -= HandleNavigationRootReached; 
```

**Add the handler method:**

```csharp
private void HandleNavigationRootReached()
{
    confirmationPopup.Show();
}
```

---

#### Option B — Trailback Event Listener

Use this when designers need to wire events without code.

Trailback include a editor shortcuts for creating a Trailback Event Listener.

Right-click anywhere in the **Hierarchy**:

```text
Trailback
└── Create Event Listener
```

This creates a **Trailback Event Listener**, which lets you respond to the **OnNavigationRootReached** event through the Inspector using UnityEvents.

<img width="680" height="762" alt="Updating Trailback Components" src="https://github.com/user-attachments/assets/f526528f-9a73-496c-b96f-b7f04814c366" />

Inside your `DemoNavigationController` or any equivalent script managing navigation, add the following method:

```csharp
private void HandleNavigationRootReached()
{
    confirmationPopup.Show();
}
```

Select the GameObject containing the Trailback Event Listener and locate the OnNavigationRootReached UnityEvent in the Inspector. Drag the GameObject containing your DemoNavigationController or equivalent navigation script into the event field, then select HandleNavigationRootReached() as the callback.

---

#### Option C — Direct Event Subscription

Inside your `DemoNavigationController` or any equivalent class managing navigation.

**Add ONE line to your existing `OnEnable()`, if there is no `OnEnable()` method then create new `OnEnable()` method:**

```csharp
TrailbackFacade.OnNavigationRootReached += HandleNavigationRootReached;
```

**Add ONE line to your existing `OnDisable()`, if there is no `OnDisable()` method then create new `OnDisable()` method:**

```csharp
TrailbackFacade.OnNavigationRootReached -= HandleNavigationRootReached; 
```

**Add the handler method:**

```csharp
private void HandleNavigationRootReached()
{
    confirmationPopup.Show();
}
```

```text
Home Screen
      ↓
Press Back
      ↓
OnNavigationRootReached
      ↓
HandleNavigationRootReached()
      ↓
confirmationPopup.Show()
```

With everything connected, pressing **Back** on the Home Screen raises the **OnNavigationRootReached** event. Instead of trying to navigate any further, the Navigation Controller displays the confirmation popup, giving your application a chance to decide what happens next.

> [!Important]
>
> This example uses `HandleNavigationRootReached()` to demonstrate the `OnNavigationRootReached` event. You can replace this method with any function that best fits your application's navigation flow and implement the behavior your application requires.


## Input Integration

Trailback supports a variety of input methods. To implement custom input, Trailback provides the abstract class `BackInputSource`. 

The `BackInputSource` class provides the base implementation for back input. It is used internally by Trailback to forward back navigation requests to the framework. As a MonoBehaviour, it is attached to a GameObject where developers can configure the input behavior and create custom input implementations.

When a back action is detected, the `BackInputSource` raises the `BackRequested` event. From there, your Navigation Controller decides what to do with the request and forwards it to Trailback through the integration bridge.

```text
Back Button
      ↓
BackInputSource
      ↓
BackRequested Event
      ↓
Navigation Controller
      ↓
TrailbackIntegrationBridge
      ↓
TrailbackFacade
```

Trailback supports two input workflows. Choose the approach that best fits your project's architecture and team.

| Event           | Workflow                                  | Recommended For                         |
| --------------- | ----------------------------------------- | --------------------------------------- |
| `BackRequested` | Inspector (UnityEvent)                    | Designers, rapid prototyping            |
| `BackRequested` | Code (`inputSource.BackRequested += ...`) | **Recommended** for production projects |

### Option A — Inspector (UnityEvent)

Use this when you want to connect the back input to your navigation controller through the Inspector.

Select the GameObject containing your `BackInputSource` and locate the `OnBackRequested` UnityEvent in the Inspector.

Drag the GameObject containing your **Navigation Controller** into the event field and select the `HandleBackRequested()` method.

```csharp
private void HandleBackRequested()
{
    TrailbackFacade.Back();
}
```

The resulting flow is:

```text
Back Input
      ↓
BackInputSource
      ↓
BackRequested UnityEvent
      ↓
HandleBackRequested()
      ↓
TrailbackFacade.Back()
```

### Option B — Code Subscription

Use this when you want to manage the event subscription in code.

Inside your **Navigation Controller**, subscribe to BackRequested in `OnEnable()`:

```csharp
private void OnEnable()
{
    backInputSource.BackRequested += HandleBackRequested;
}
```

Unsubscribe from the event in `OnDisable()`:

```csharp
private void OnDisable()
{
    backInputSource.BackRequested -= HandleBackRequested;
}
```

Add the handler method:

```csharp
private void HandleBackRequested()
{
    TrailbackFacade.Back();
}
```

>[!IMPORTANT]
> 
> Choose only one workflow for handling `BackRequested`. **Do not subscribe to the event through both the Inspector and code, as this will cause the navigation request to be handled more than once.**

**Responsibilities**

`BackInputSource` has a single responsibility:

* Detect back input.
* Raise the `BackRequested` event.

It does **not**:

* Execute navigation.
* Manage navigation history.
* Show or hide UI.

Keeping those responsibilities separate means Trailback doesn't depend on a particular input framework. Whether your project uses the Legacy Input Manager, the Unity Input System, VR controllers, touch gestures, or a custom input solution, the integration stays the same—the input source raises `BackRequested`, and the rest of the navigation flow remains unchanged.

Trailback includes ready-to-use implementations for both the **[Legacy Input Manager](ReferenceSamplesGuide.md#legacy-input)** and the **[Unity Input System](ReferenceSamplesGuide.md#unity-input-system)**. The complete setup and implementation for each input system is covered in the **[Reference Samples Guide](ReferenceSamplesGuide.md)**.

> [!TIP]
>
> The input implementations are documented only once to keep the documentation consistent and easier to maintain. 


## Resetting Navigation History

During the lifetime of an application there are multiple times where the developers have to change scenes. After reloading the current scene or loading a new scene the history becomes stale after a scene change, and it will cause errors when navigating back. To prevent this, reset the navigation history when switching scenes.

Different projects use different scene management workflows, such as single-scene or additive-scene loading. Based on your projects workflow the developers should choose the proper time to reset navigation history.

```csharp
 public override void ResetHistory()
{
    TrailbackFacade.ResetHistory();
}
```

> [!Note]
>
> Trailback intentionally leaves this decision to your application so it can support different scene management workflows.


# Next Steps

Continue with the guide that best matches what you're working on.

| Guide                                                   | Description                                                                                         |
|---------------------------------------------------------|-----------------------------------------------------------------------------------------------------|
| [**README**](../../README.md#table-of-contents)         | Return to the main documentation for installation, project requirements, and the Quick Start guide. |
| [**Reference Samples Guide**](ReferenceSamplesGuide.md) | Explore the included reference implementations and learn from complete integration examples.        |
| [**Handling Scene Changes**](HandlingSceneChanges.md)   | Learn how to manage navigation history across scene changes and multi-scene workflows.              |
| [**Troubleshooting**](Troubleshooting.md)               | Resolve common setup, configuration, and integration issues.                                        |
| [**Features**](Features.md)                             | Return to the main documentation, installation instructions, and Quick Start guide. |
