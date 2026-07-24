# Trailback

> [!WARNING]
>
> ## Trailback v1.0.0-alpha
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
   * [Step 3: Open the Complete UGUI Demo](#step-3--open-the-complete-ugui-demo)

4. [Complete UGUI Demo](#complete-ugui-demo)

### Getting Started

5. [Understanding the Navigation Flow](#understanding-the-navigation-flow)
6. [Creating Trailback Components](#creating-trailback-components)

### Quick Start — Build Your First Integration

7. [Build Your First Trailback Integration](#build-your-first-trailback-integration)
   * [Step 1: Create the Application Structure](#step-1--create-the-application-structure)
   * [Step 2: Create Navigation Categories](#step-2--create-navigation-categories)
   * [Step 3: Create UI Structure](#step-3--create-ui-structure)
   * [Step 4: Create a Reusable Base UI Class](#step-4--create-a-reusable-base-ui-class)
   * [Step 5: Create Screens](#step-5--create-screens)
   * [Step 6: Create Popups](#step-6--create-popups)
   * [Step 7: Create a Navigation Handler](#step-7--create-a-navigation-handler)
   * [Step 8: Create a Bridge](#step-8--create-a-bridge)
   * [Step 9: Create a Navigation Controller](#step-9--create-a-navigation-controller)
   * [Step 10: Connect Back Input](#step-10--connect-back-input)
   * [Step 11: Handle OnNavigationRootReached Event](#step-11--handle-onnavigationrootreached-event)
   * [Step 12: Verify Your Integration](#step-12--verify-your-integration)

### Advanced Topics

8. [Connecting Back Input](#connecting-back-input)
9. [Event Subscription Reference](#event-subscription-reference)
10. [OnNavigationRootReached Event](#onnavigationrootreached-event)
11. [Adding Popups and Priorities](#adding-popups-and-priorities)
12. [Adding Navigation Blockers](#adding-navigation-blockers)

### Documentations

13. [Documentation](#documentation)

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

It manages navigation history, resolves which UI element should respond to Back, and lets your application decide how that navigation is performed. Trailback doesn't manage your UI—it simply determines where Back should go next.

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

| Feature                                                                                       | Description                                                                                                                                                           |
|-----------------------------------------------------------------------------------------------|-----------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| [**Navigation History**](Documentation/Guides/Features.md#navigation-history)                 | Keeps track of screens and popups, making it easy to return to previously visited UI.                                                                                 |
| [**Navigation Categories**](Documentation/Guides/Features.md#navigation-categories)           | Organize different UI layers and control which one responds first to Back navigation.                                                                                 |
| [**Navigation Blocking**](Documentation/Guides/Features.md#navigation-blocking)               | Temporarily disable Back navigation while users complete a required workflow.                                                                                         |
| [**Root Protection**](Documentation/Guides/Features.md#root-protection)                       | Keep the root of your navigation flow in place instead of navigating past it.                                                                                         |
| [**OnNavigationRootReached Event**](Documentation/Guides/Features.md#navigation-root-reached) | Notify your application when the user reaches the beginning of the navigation history.                                                                                |
| [**Input Abstraction**](Documentation/Guides/Features.md#input-abstraction)                   | Use the input system that already fits your project through `BackInputSource`.                                                                                        |
| [**Bridge-Based Integration**](Documentation/Guides/Features.md#bridge-based-integration)     | Keep Trailback isolated behind a dedicated integration layer instead of calling the framework throughout your application.                                            |
| [**Scene Reload Support**](Documentation/Guides/Features.md##scene-reload-support)            | Reset and rebuild navigation history whenever your application starts a new navigation flow, including scene reloads, level transitions, or returning to a main menu. |
| [**UI Framework Agnostic**](Documentation/Guides/Features.md#ui-framework-agnostic)           | Trailback manages navigation state rather than UI rendering, allowing it to integrate with UGUI, UI Toolkit, and custom UI frameworks without imposing a specific UI architecture.                                                                                                                                                                      |

If you'd like a closer look at how these features work, continue with the **Features Guide**.

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

> Make navigation behavior predictable, extensible, and easy to debug.

---

## Installing Trailback

### Requirements

> [!IMPORTANT]
>
> Trailback is developed and tested with **UGUI**.
>
> While the core framework isn't tied to a specific UI system, the included **Complete UGUI Demo** and **Runtime Monitor** reference Complete UGUI Demo are currently built with UGUI.
> 
> Before importing the **Complete UGUI Demo**, make sure the **TextMeshPro** and **Input System** packages are installed if you plan to use the included reference samples. This avoids compilation errors caused by missing package dependencies.

Trailback has **no required package dependencies**.

Some reference samples may require additional Unity packages depending on what they demonstrate. Refer to the **Reference Samples Guide** for sample-specific requirements, setup instructions, and supported workflows.

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

The Complete UGUI Demo includes working reference implementations for:

* Navigation History
* Navigation Categories
* Navigation Blocking
* Root Protection
* OnNavigationRootReached Event
* Legacy Input integration
* Unity Input System integration
* Runtime Monitor

https://github.com/user-attachments/assets/1837d456-d5ea-4edc-988c-367dd89d9195

The Complete UGUI Demo is strongly recommended for first-time users and alpha testers.

> [!NOTE]
>
> Reference samples are optional.
>
> You can use Trailback without importing any of them. Simply import the samples that match the features or workflows you want to explore.

---

### Step 3 — Open The Complete UGUI Demo

After importing the sample:

```text
Assets
    → Samples
        → Trailback
            → [1.0.0-alpha]
                → Trailback UGUI Demo
```

Open the **Complete UGUI Demo** and press **Play**.

The Complete UGUI Demo brings Trailback's core features together in a complete navigation flow, making it a great place to see how the framework behaves before integrating it into your own project.

The Complete UGUI Demo serves as both:

* Feature Showcase
* Reference Implementation
* Integration Reference

and is the fastest way to learn how Trailback is intended to be configured and integrated.

Looking for a specific sample?

The [**Reference Samples Guide**](Documentation/Guides/ReferenceSamplesGuide.md) explains what each Reference implementation demonstrates, when to use it, and how to set it up in your project.

---

## Complete UGUI Demo

The package includes the **Complete UGUI Demo**, a fully working project that brings Trailback's core features together in a realistic navigation flow.

Many of the examples throughout the documentation are based on this project, so it's a great place to explore the framework, experiment with different scenarios, and compare your own integration against a working example.

If you're new to Trailback, the **Complete UGUI Demo** is the best place to begin.

Rather than exploring features one by one, you'll see them working together in a complete project. It's a great way to get familiar with the framework before integrating it into your own application.

https://github.com/user-attachments/assets/a1d0ad52-c798-4c95-98a0-bc0ae1ed2504

> [!TIP]
>
> The **Complete UGUI Demo** is the reference project used throughout the documentation.
>
> If you're following the [**Quick Start**](#quick-start--build-your-first-integration), [**Troubleshooting**](Documentation/Guides/Troubleshooting.md), or [**Handling Scene Changes**](Documentation/Guides/HandlingSceneChanges.md) guides and something doesn't look right, compare your project with the Complete UGUI Demo. It's a reliable way to verify your setup and catch missing configuration.

### Core Features
* [Navigation History](Documentation/Guides/Features.md#navigation-history)
* [Navigation Categories](Documentation/Guides/Features.md#navigation-categories)
* [Navigation Blocking](Documentation/Guides/Features.md#navigation-blocking)
* [Root Protection](Documentation/Guides/Features.md#root-protection)
* [OnNavigationRootReached](Documentation/Guides/Features.md#navigation-root-reached)
* [Input Abstraction](Documentation/Guides/Features.md#input-abstraction)
* [Bridge-Based Integration](Documentation/Guides/Features.md#bridge-based-integration)
* [Scene Reload Support](Documentation/Guides/Features.md#scene-reload-support)
* [UI Framework Agnostic](Documentation/Guides/Features.md#ui-framework-agnostic)

### Included Reference Implementations
* [Legacy Input](Documentation/Guides/ReferenceSamplesGuide.md#legacy-input)
* [Unity Input System](Documentation/Guides/ReferenceSamplesGuide.md#unity-input-system)
* [Runtime Monitor](Documentation/Guides/ReferenceSamplesGuide.md#runtime-monitor)

### What You'll See

The Complete UGUI Demo includes working examples of:

* Screens
* Popups
* Multiple Navigation Categories
* Back Input Sources
* Runtime Monitor

This makes it easy to observe how Trailback responds to navigation requests in different situations and how multiple navigation features interact with one another.

### What You'll Learn From The Code

The included scripts provide practical examples of:

* Implementing `IBackNavigable`
* Creating Navigation Categories
* Creating Navigation Handlers
* Creating a Trailback Integration Bridge
* Connecting Back Input Sources
* Implementing `IBackNavigationBlocker`
* Responding to Root Reached Events

### Recommended Learning Order

```text
Import Complete UGUI Demo
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

The **Complete UGUI Demo** is designed to be explored and adapted as you learn Trailback.

Many of the examples throughout the documentation are based on this project, so becoming familiar with it will make the rest of the guides easier to follow. It's also a useful reference whenever you want to compare your own integration with a working example.

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
TrailbackIntegrationBridge
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
> The **Complete UGUI Demo** is the reference project used throughout the documentation.
>
> If you're following the [**Quick Start**](#quick-start--build-your-first-integration) or [**Troubleshooting**](Documentation/Guides/Troubleshooting.md) guide and something isn't working as expected, compare your project with the Complete UGUI Demo. It's the easiest way to verify your setup and spot any missing configuration.

---

## Creating Trailback Components

Trailback includes a few editor shortcuts for creating commonly used components.

Right-click anywhere in the **Hierarchy**:

```text
Trailback
└── Create Event Listener
```

This creates a **Trailback Event Listener**, which lets you respond to the **OnNavigationRootReached** event through the Inspector using UnityEvents.

This is particularly useful for:

* Designer-driven workflows
* Exit confirmation dialogs
* Playing UI sounds or animations
* Projects that prefer Inspector-based configuration

<img width="680" height="762" alt="Updating Trailback Components" src="https://github.com/user-attachments/assets/f526528f-9a73-496c-b96f-b7f04814c366" />

> [!NOTE]
>
> The Trailback Event Listener is completely optional.
>
> If you're already handling the **OnNavigationRootReached** event in your Navigation Controller or through a `TrailbackIntegrationBridge`, there's no need to add an Event Listener.

Some reference samples include additional editor tools to simplify their setup. These utilities are sample-specific and aren't part of the core Trailback framework.

## Build Your First Trailback Integration

This guide takes you through the process of integrating Trailback into a project from the ground up.

By the end, you'll have a working navigation setup with screens, popups, navigation blockers, root navigation handling, and back input fully connected and ready to build on.

```text
2 Screens
2 Popups (one of which blocks back navigation)
1 Navigation Handler
1 Bridge
1 Navigation Controller
```

By the end, you'll be able to open screens and popups, press Back to return through history correctly, and confirm that a blocked popup stops navigation until dismissed.

### Step 1 — Create the Application Structure

Before adding Trailback, create the objects that will handle navigation in your scene.

Your hierarchy should look like this:

```text
Scene
├── Navigation Controller
├── Back Input Source
└── Event Listener (Optional)
```

#### Navigation Controller

Create an empty GameObject named **Navigation Controller**.

This object acts as the entry point for your application's navigation. It will:

* Receive back navigation requests.
* Report navigation changes to Trailback.
* Register the application's navigation handler.
* Coordinate your project's navigation logic.

Later in this guide, you'll attach the `DemoNavigationController` component to this GameObject.

#### Back Input Source

Create a GameObject with the `BackInputSource` implementation you want to use.

Trailback doesn't depend on a specific input system, so you can choose the approach that matches your project.

The **Reference Samples Guide** includes complete examples for:

* [Legacy Input](Documentation/Guides/ReferenceSamplesGuide.md#legacy-input)
* [Unity Input System](Documentation/Guides/ReferenceSamplesGuide.md#unity-input-system)

You'll connect the `BackRequested` event to the Navigation Controller in [**Step 10**](#step-10--connect-back-input).

#### Trailback Event Listener (Optional)

If you'd like to respond to the **OnNavigationRootReached** event from the Inspector, create a Trailback Event Listener.

```text
GameObject
    → Trailback
        → Create Event Listener
```

This is useful for designer-driven workflows or UnityEvents. If you plan to handle the event in code or through your integration bridge, you can skip this step.

Once these objects are in place, you're ready to create your navigation categories.


### Step 2 — Create Navigation Categories

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


**UI Category:**

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

`Protect Root Element = true` prevents the root navigation element from being removed from the navigation history. When Back is pressed at the beginning of the navigation flow, Trailback raises the **OnNavigationRootReached** event instead of attempting another back navigation.

<img width="539" height="350" alt="Root Protection" src="https://github.com/user-attachments/assets/bfc7c401-a990-4e11-b294-02726e790444" />

> [!IMPORTANT]
>
> In most projects, only **one** navigation category should have **Protect Root Element** enabled.
>
> This category acts as the root of your navigation flow. Enabling Root Protection on multiple categories can make it unclear which category should stop back navigation.
>
> To learn more about how Root Protection works and when to use it, see the **Features Guide** → [Root Protection](Documentation/Guides/Features.md#root-protection).


Higher priority categories resolve first:

```text
Back Requested
      ↓
Popup Category (Priority 100)
      ↓
UI Category (Priority 0)
```

These priorities are used throughout the Quick Start and the Complete UGUI Demo. Your own project can use different values depending on how you organize your navigation.

### Step 3 — Create UI Structure

In your scene, create:

1. A Canvas (if you don't have one)
2. Under the Canvas, create a Panel for each screen/popup:
   * HomeScreen
   * SettingsScreen 
   * InfoPopup 
   * LockedPopup
   * Confirmation Popup
3. Add a Button to HomeScreen: "Open Settings" → wire to `DemoNavigationController.OpenSettings()`
4. Add a Button to SettingsScreen: "Show Info" → wire to `DemoNavigationController.OpenInfoPopup()`
5. Add a Button to InfoPopup: Show Locked Popup → wire to `DemoNavigationController.OpenLockedPopup()`

Each panel needs:
* Canvas component
* CanvasGroup component
* GraphicRaycaster
* CanvasScaler
* The corresponding UI subclass (HomeScreen, SettingsScreen, etc.)

### Step 4 — Create A Reusable Base UI Class

```csharp
using UnityEngine;
using ModularForge.Trailback.Core;

[RequireComponent(typeof(Canvas))]
[RequireComponent(typeof(CanvasGroup))]
public class UIBase : MonoBehaviour, IBackNavigable
{
    private Canvas _canvas;
    private CanvasGroup _canvasGroup;

    [field: SerializeField]
    public NavigationCategorySo NavigationCategory { get; private set; }

    protected virtual void Awake()
    {
        _canvas = GetComponent<Canvas>();
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    public virtual void Show()
    {
        _canvas.enabled = true;
        SetCanvasGroupVisible(true);
    }

    public virtual void Hide()
    {
        _canvas.enabled = false;
        SetCanvasGroupVisible(false);
    }

    private void SetCanvasGroupVisible(bool isVisible)
    {
        _canvasGroup.alpha = isVisible ? 1f : 0f;
        _canvasGroup.interactable = isVisible;
        _canvasGroup.blocksRaycasts = isVisible;
    }
}
```

> `UIBase` is a reusable base implementation shared by screens, popups, and any other navigable UI element. This implementation is intentionally simplified for learning — the Complete UGUI Demo included with Trailback contains a more complete, production-ready version.

Every subclass must have its `NavigationCategory` assigned through the Inspector.

### Step 5 — Create Screens

```csharp
/// <summary>
/// Represents the application's home screen.
/// </summary>
public class HomeScreen : UIBase 
{
     // Home Screen-specific behavior would typically live here.
}
```

```csharp
/// <summary>
/// Represents the application's settings screen.
/// </summary>
public class SettingsScreen : UIBase 
{
    // Settings Screen-specific behavior would typically live here.
}
```

Assign the **UI** category to both.

### Step 6 — Create Popups

**InfoPopup** (can be closed via Back or a Close button):

```csharp
using ModularForge.Trailback.Core;
using UnityEngine;

public class InfoPopup : UIBase
{
    public void Close()
    {
        Hide();
        TrailbackFacade.ReportHidden(this);
    }
}
```

**LockedPopup** (Back is blocked; must be dismissed explicitly):

```csharp
using ModularForge.Trailback.Core;
using UnityEngine;

public class LockedPopup : UIBase, IBackNavigationBlocker
{
    [field: SerializeField]
    public BackNavigationMode BackNavigationMode { get; private set; } = BackNavigationMode.Block;

    public void Dismiss()
    {
        Hide();
        TrailbackFacade.ReportHidden(this);
    }
}
```

> [!Note]
> 
> This Quick Start calls TrailbackFacade directly to keep the example simple and focused on the core API.
> 
> In a real project, you'll usually handle navigation through a central controller or navigation manager instead of calling the framework directly from your UI. The included Complete UGUI Demo project shows one way to structure this.

Assign the **Popup** category to both.

**To make a popup block navigation, two things must both be true:**

1. It implements `IBackNavigationBlocker`.
2. `BackNavigationMode` is set to `Block` in the Inspector.

If either is missing, the popup will not block navigation.

**Scene Setup:**

1. Create UI elements for both popups with:
   - A close/dismiss button
   - Button → OnClick → call `InfoPopup.Close()` or `LockedPopup.Dismiss()`

When `Close()` or `Dismiss()` is called, the popup hides visually AND removes
itself from Trailback's history, so the next Back press goes to the previous screen.

### Step 7 — Create A Navigation Handler

The handler decides *how* navigation executes once Trailback has resolved *what* should happen next.

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

        if (context.Previous is UIBase previousUI)
        {
            previousUI.Show();
        }
    }
}
```

> **Avoid directly enabling and disabling GameObjects inside navigation handlers.** Prefer a UI abstraction such as `UIBase.Show()` / `UIBase.Hide()`, as shown above. This keeps navigation behavior separate from UI implementation details.

### Step 8 — Create A Bridge

The integration bridge acts as the connection between your application and Trailback.

Instead of calling the framework directly throughout your project, your application communicates with the bridge. The bridge then forwards those requests to Trailback, giving you a single place to manage the integration.

Keeping that boundary in one place makes your navigation code easier to maintain and gives you the flexibility to change or extend your implementation later if your project's architecture evolves.

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

### Step 9 — Create A Navigation Controller

The Navigation Controller coordinates your application's back navigation.

It receives back requests from the input source, reports navigation changes through the integration bridge, and handles the navigation decisions returned by Trailback.

In most projects, this becomes the central place where your application's navigation logic and Trailback come together.

```csharp
using ModularForge.Trailback.Core;
using UnityEngine;

public class DemoNavigationController : MonoBehaviour
{
    private DemoTrailbackBridge _bridge;

    [SerializeField] private HomeScreen homeScreen;
    [SerializeField] private SettingsScreen settingsScreen;
    
    [SerializeField] private InfoPopup infoPop;
    [SerializeField] private LockedPopup lockedPopup;

    private void Awake()
    {   
        _bridge = new DemoTrailbackBridge();

        // Initialize the session once during startup
        _bridge.InitializeSession();
        
        // Register the application's navigation handler once during startup.
        _bridge.SetNavigationHandler(new DemoBackNavigationHandler());
    }

    private void Start()
    {
        homeScreen.Show();
        _bridge.Show(homeScreen);

        settingsScreen.Hide();
        infoPop.Hide();
        lockedPopup.Hide();
    }

    public void OpenSettings()
    {
        homeScreen.Hide();
        
        // IMPORTANT: Do NOT call _bridge.Hide(homeScreen) here.
        // To keep Home in history so pressing Back returns to it.
        // Only call _bridge.Hide() if you want to completely remove 
        // an element from history.
        
        settingsScreen.Show();
        _bridge.Show(settingsScreen);
    }

    public void OpenInfoPopup()
    {
        infoPop.Show();
        _bridge.Show(infoPop);
    }

    public void OpenLockedPopup()
    {
        lockedPopup.Show();
        _bridge.Show(lockedPopup);
    }
    
    public void HandleBackRequested()
    {
        _bridge.Back();
    }
}
```

> [!IMPORTANT]
> 
> InitializeSession() prepares Trailback for a new navigation session by clearing any previous navigation state before the first navigation element is registered. This should be called once during application startup before showing your initial screen.

> [!NOTE]
> 
> This example focuses only on the Trailback integration to keep the code easy to follow. The Complete UGUI Demo also includes application-specific code for screen management, popup management, lookup tables, and initialization, but those parts have been left out to keep the example focused.

### Step 10 — Connect Back Input

In **Step 9**, you created `DemoNavigationController`, including the `HandleBackRequested()` callback. Now it's time to connect a `BackInputSource` to that callback.

`BackInputSource` is how Trailback receives back navigation requests. It works with Unity's Legacy Input Manager, the Unity Input System, or any custom input solution that raises the `BackRequested` event.

If you haven't created a `BackInputSource` yet, the [**Reference Samples Guide**](Documentation/Guides/ReferenceSamplesGuide.md#legacy-input) includes complete examples for both supported Unity input systems.

Trailback supports two ways to connect the event:

* **Option A** — Subscribe in code *(recommended)*
* **Option B** — Connect the event in the Inspector using UnityEvents

> [!IMPORTANT]
>
> Pick **one** approach and stick with it.
>
> Don't subscribe in code **and** wire the UnityEvent in the Inspector at the same time. Otherwise, `HandleBackRequested()` will be called twice for every Back press, causing two navigation requests to be processed.

---

#### Option A — Subscribe in Code (Recommended)

If you prefer to keep event wiring in code, update the `DemoNavigationController` from **Step 9** with the following changes.

##### 1. Add a `BackInputSource` reference

```csharp
[SerializeField] private BackInputSource backInputSource;
```

This references the component that raises the `BackRequested` event.

##### 2. Subscribe to `BackRequested`

Add the following methods to your controller:

```csharp
private void OnEnable()
{
    backInputSource.BackRequested += HandleBackRequested;
}

private void OnDisable()
{
    backInputSource.BackRequested -= HandleBackRequested;
}
```

Subscribing in `OnEnable()` and unsubscribing in `OnDisable()` ensures the controller only receives events while it's active.

##### 3. Assign the `BackInputSource`

Select the **Navigation Controller** GameObject and assign your `BackInputSource` to the **Back Input Source** field.

---

#### Option B — Connect Through the Inspector

If you prefer UnityEvents, you can wire everything up directly in the Inspector.

> [!IMPORTANT]
>
> If you choose this approach, don't apply the changes from **Option A**.
>
> Leave the `DemoNavigationController` from **Step 9** unchanged.

1. Select the **BackInputSource** GameObject.
2. Find the **Back Requested** UnityEvent.
3. Click **+** to add a listener.
4. Drag the **Navigation Controller** GameObject into the object field.
5. Select **DemoNavigationController → HandleBackRequested()**.

---

Both approaches produce the same result:

```text
Back Input
      ↓
BackInputSource
      ↓
BackRequested Event
      ↓
DemoNavigationController
      ↓
TrailbackIntegrationBridge
      ↓
TrailbackFacade.Back()
```

---

#### Avoid Double Subscription

A common mistake is subscribing to the same event in both places.

For example:

* Subscribe to `BackRequested` in `OnEnable()`.
* Also assign `HandleBackRequested()` to the **Back Requested** UnityEvent in the Inspector.

That causes a single Back press to invoke `HandleBackRequested()` twice.

```text
Back Button
      ↓
BackRequested
      │
 ┌────┴────┐
 ↓         ↓
Code   Inspector
 ↓         ↓
HandleBackRequested()
HandleBackRequested()
```

This can lead to:

* Two back navigation requests from a single button press.
* Multiple screens or popups closing at once.
* Navigation history becoming inconsistent.

Choose either **code subscription** or **Inspector subscription**—never both.

> [!TIP]
>
> Need a `BackInputSource`?
>
> The [**Reference Samples Guide**](Documentation/Guides/ReferenceSamplesGuide.md) includes complete examples for both the Legacy Input Manager and the Unity Input System.

---

> [!TIP]
>
> ## Checkpoint — Core Navigation
>
> Before moving on, press **Play** and make sure the basic navigation flow is working.
>
> You should see the following:
>
> * ☑ The Home Screen is visible when the scene starts.
> * ☑ Clicking **Open Settings** opens the Settings Screen.
> * ☑ Clicking **Show Info** opens the Info Popup.
> * ☑ Pressing **Back** closes the Info Popup.
> * ☑ Pressing **Back** again returns to the Home Screen.
> * ☑ Opening the **Locked Popup** and pressing **Back** does not close it.
> * ☑ Closing the **Locked Popup** restores normal back navigation.
>
> If anything behaves differently, compare your setup with the **Complete UGUI Demo** or work through the **Troubleshooting Guide** before continuing.

### Step 11 — Handle OnNavigationRootReached Event

When the user reaches the beginning of the navigation history and presses Back again, Trailback raises the OnNavigationRootReached event instead of attempting another back navigation.

```text
Home Screen 
    ↓
Press Back 
    ↓ 
OnNavigationRootReached Event 
    ↓ 
Your Application Decides What Happens Next
```

https://github.com/user-attachments/assets/63ad1f89-9119-4ed8-b920-3a870fac7504

Choose how to respond using one of these approaches:

| Approach                      | Best For                                           |
| ----------------------------- | -------------------------------------------------- |
| **Bridge**                    | Production projects (Recommended)                  |
| **Trailback Event Listener**  | Designer-driven workflows                          |
| **Direct Event Subscription** | Small projects, prototypes                         |

Trailback provides three ways to respond when the navigation root is reached.

#### Example: Adding an Exit Confirmation Popup

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

Add a reference to the confirmation popup in your `DemoNavigationController`:

```csharp
[SerializeField] private ConfirmationPopup confirmationPopup;
```

In the start function add the following line to hide the confirmationPopup

```csharp
confirmationPopup.Hide();
```

Then assign the **Confirmation Popup** GameObject to this field in the Inspector.

Your scene hierarchy should now look like this:

```text
Canvas
├── Home Screen
├── Settings Screen
├── Info Popup
├── Locked Popup
└── Confirmation Popup
```

Choose one of the following approaches that best fits your project.

#### Option A — Subscribe Through a Bridge (Recommended)

In Step 10, you subscribed to the back input event. Now add a second subscription for the root reached event. You're not replacing anything — just adding to the same method.

**Add ONE line to your existing `OnEnable()`:**

```csharp
private void OnEnable()
{
    backInputSource.BackRequested += HandleBackRequested;  // ← From Step 9
    _bridge.OnNavigationRootReached += HandleNavigationRootReached;    // ← NEW: Add this
}
```

**Add ONE line to your existing `OnDisable()`:**

```csharp
private void OnDisable()
{
    backInputSource.BackRequested -= HandleBackRequested;  // ← From Step 9
    _bridge.OnNavigationRootReached -= HandleNavigationRootReached;    // ← NEW: Add this
}
```

**Add the handler method (this is new):**

```csharp
private void HandleNavigationRootReached()
{
    confirmationPopup.Show();
}
```

#### Option B — Trailback Event Listener

Use this when designers need to wire events without code.

Create an Event Listener:
```text
GameObject
    → Trailback
        → Create Event Listener
```

Assign a callback in the Inspector: `OnNavigationRootReached → Show Exit Confirmation Popup`

#### Option C — Direct Event Subscription

Subscribe directly to the framework (not recommended for production):

```csharp
private void OnEnable()
{
    TrailbackFacade.OnNavigationRootReached += HandleNavigationRootReached;
}

private void OnDisable()
{
    TrailbackFacade.OnNavigationRootReached -= HandleNavigationRootReached;
}

private void HandleNavigationRootReached()
{
    confirmationPopup.Show();
}
```

When the **OnNavigationRootReached** event is raised, the Navigation Controller shows the confirmation popup.

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

Now when the user reaches navigation root and presses Back, your confirmation popup appears.

> [!TIP]
>
> ## Checkpoint — OnNavigationRootReached Event
>
> Before moving on to the final step, press **Play** and make sure the **OnNavigationRootReached** event flow is working as expected.
>
> You should see the following:
>
> ☑ Pressing **Back** on the **Home Screen** raises the **OnNavigationRootReached** event.
>
> ☑ The **Confirmation Popup** appears.
>
> ☑ Clicking **Stay** closes the popup and returns to the **Home Screen**.
>
> ☑ Clicking **Exit** runs your application's exit logic.
>
> ☑ Pressing **Back** while the **Confirmation Popup** is open closes the popup before returning to the **Home Screen**.
>
> If something behaves differently, go back and review **Step 10** before continuing.

### Step 12 — Verify Your Integration

At this point, everything should be connected. Press Play and walk through the scenarios below to make sure the navigation flow behaves as intended.

#### Scenario 1 — Navigation History

```text
Home Screen
        ↓
Open Settings
        ↓
Open Info Popup
        ↓
Press Back
        ↓
Info Popup closes
        ↓
Settings Screen remains visible
        ↓
Press Back
        ↓
Home Screen becomes visible
```

Each Back action should return to the previously visited UI element, demonstrating that the navigation history is being tracked correctly.

#### Scenario 2 — Navigation Blockers

```text
Home Screen 
    ↓ 
Open Locked Popup 
    ↓ 
Press Back 
    ↓ 
Navigation Blocked 
    ↓ 
Dismiss Popup 
    ↓ 
Press Back 
    ↓ 
Home Screen
```

While the popup is active, Back should have no effect. Once the popup is dismissed, navigation should continue as normal.

#### Scenario 3 — OnNavigationRootReached Event

```text
Home Screen 
    ↓ 
Press Back 
    ↓ 
OnNavigationRootReached 
    ↓ 
Exit Confirmation Popup
```

Select Stay to close the confirmation popup and remain on the Home Screen.

Select Exit to run your application's exit logic.

Reaching the root of the navigation history should raise the OnNavigationRootReached event instead of attempting another back navigation.

#### Expected Result

If each scenario produces the expected result, your Trailback integration is working correctly and can be used as the foundation for your own UI flow.

**If a scenario doesn't work:**

* Compare your project with the **Complete UGUI Demo** to verify your setup.
* Verify all GameObjects are assigned in the Inspector
* Check the Console for errors or warnings
* If you still can't identify the problem, see the **Troubleshooting Guide** for common integration issues and debugging steps.

---

> [!TIP]
>
> ## 🎉 Quick Start Complete
>
> Your Trailback integration is up and running.
>
> You're now ready to adapt it to your own project or dive deeper into the documentation. The guides below cover the framework's core features, reference samples, scene management, and common troubleshooting scenarios.
>
> 📚 Continue with the **[Documentation](#documentation)** section below.

---

## Connecting Back Input

> For the complete setup, see **[Step 10 — Connect Back Input](#step-10--connect-back-input)**. This section explains how `BackInputSource` fits into Trailback's navigation flow.

Trailback doesn't read input directly. Instead, it relies on a `BackInputSource` to detect when the user requests a back navigation.

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

### Responsibilities

`BackInputSource` has a single responsibility:

* Detect back input.
* Raise the `BackRequested` event.

It does **not**:

* Execute navigation.
* Manage navigation history.
* Show or hide UI.

Keeping those responsibilities separate means Trailback doesn't depend on a particular input framework. Whether your project uses the Legacy Input Manager, the Unity Input System, VR controllers, touch gestures, or a custom input solution, the integration stays the same—the input source raises `BackRequested`, and the rest of the navigation flow remains unchanged.

If you're implementing your own input source, see the **Reference Samples Guide** for complete `BackInputSource` implementations using both the Legacy Input Manager and the Unity Input System.

📄 [Reference Samples Guide](Documentation/Guides/ReferenceSamplesGuide.md#legacy-input)

## Event Subscription Reference

Trailback supports more than one way to subscribe to its events, so you can pick the workflow that fits your team.

| Event | Workflow                                                  | Recommended For |
|---|-----------------------------------------------------------|---|
| `BackRequested` | Inspector (UnityEvent)                                    | Designers, rapid prototyping |
| `BackRequested` | Code (`inputSource.BackRequested += ...`)                 | Programmers, production projects — used throughout this guide |
| `OnNavigationRootReached` | Inspector via `TrailbackEventListener`                    | Designers, UI/audio reactions |
| `OnNavigationRootReached` | Bridge (`_bridge.OnNavigationRootReached += ...`)                     | **Recommended.** Keeps application code decoupled from the framework. |
| `OnNavigationRootReached` | Direct (`TrailbackFacade.OnNavigationRootReached += ...`) | Fully supported, but creates a direct dependency on Trailback. Prefer the bridge for production code. |

**Code subscription example** (used by the Quick Start above):

```csharp
private void OnEnable()
{
    inputSource.BackRequested += HandleBackRequested;
}

private void OnDisable()
{
    inputSource.BackRequested -= HandleBackRequested;
}
```

**Inspector subscription:** assign a callback to the `BackInputSource`'s exposed event field directly in the Inspector — no code required. This is the same mechanism used in [Step 10](#step-10--connect-back-input).

**Root Reached, via Inspector:** add a `TrailbackEventListener` component to any GameObject and assign UnityEvent callbacks — useful for playing a sound or showing an exit confirmation without writing code.

---

## OnNavigationRootReached Event

When Back navigation reaches the protected root of the active navigation category, Trailback raises the `OnNavigationRootReached` event instead of trying to navigate any further.

```text
Navigation History

Home Screen
      │ Back
      ↓
OnNavigationRootReached Event
      ↓
Your Application Decides What Happens Next
```

At this point, Trailback stops processing the navigation request and hands control back to your application.

Common responses include:

* Showing an exit confirmation dialog
* Returning to the main menu
* Exiting the application
* Recording analytics events
* Playing UI or audio feedback

Trailback supports three ways to respond to this event.

### Inspector Workflow

**Recommended for:** Designer-driven workflows and Inspector configuration.

Create a Trailback Event Listener using either of the following approaches.

**Option A (Recommended)**

```text
GameObject
    → Trailback
        → Create Event Listener
```

**Option B**

Add the `TrailbackEventListener` component to any GameObject and configure its callbacks in the Inspector.

Example:

```text
OnNavigationRootReached
        ↓
Show Exit Confirmation Popup
```

or

```text
OnNavigationRootReached
        ↓
Play UI Sound
```

Event flow:

```text
TrailbackFacade
        ↓
OnNavigationRootReached
        ↓
TrailbackEventListener
        ↓
UnityEvent
        ↓
Inspector Callback
```

This option is useful when designers or artists need to react to navigation events without writing code.

---

### Bridge Workflow (Recommended)

**Recommended for:** Production projects.

Subscribe through your `TrailbackIntegrationBridge`.

```csharp
private void OnEnable()
{
    _bridge.OnNavigationRootReached += HandleRootReached;
}

private void OnDisable()
{
    _bridge.OnNavigationRootReached -= HandleRootReached;
}

private void HandleRootReached()
{
    confirmationPopup.Show();
}
```

Using the bridge keeps Trailback-specific code in one place and avoids introducing framework dependencies throughout the rest of your application.

---

### Direct Framework Subscription

**Recommended for:** Small projects, prototypes, or quick experiments.

```csharp
private void OnEnable()
{
    TrailbackFacade.OnNavigationRootReached += HandleRootReached;
}

private void OnDisable()
{
    TrailbackFacade.OnNavigationRootReached -= HandleRootReached;
}

private void HandleRootReached()
{
    Debug.Log("[Trailback] On Navigation root reached Event.");
}
```

```text
TrailbackFacade
        ↓
Application Code
```

This approach is fully supported, but it couples your application directly to `TrailbackFacade`. For larger projects, subscribing through the integration bridge is generally the better long-term choice.


## Adding Popups And Priorities

Categories with a higher priority resolve before lower-priority ones:

```text
Popup Priority = 100
UI Priority = 0
```

```text
Settings Screen
        ↓
Confirmation Popup
```

Press Back:

```text
Confirmation Popup closes
        ↓
Settings Screen remains visible
```

Trailback resolves the popup first because it belongs to a higher-priority category — you don't need to manage this ordering yourself.

---

## Adding Navigation Blockers

Sometimes navigation should not continue until the user makes an explicit choice — a purchase confirmation, an unsaved-changes warning, a terms-acceptance screen.

```csharp
public sealed class PurchaseConfirmationPopup : UIBase, IBackNavigationBlocker
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

Navigation will not continue until the blocking element is dismissed by the user — see [Step 6](#step-6--create-popups) for the full requirements.

https://github.com/user-attachments/assets/35a5a1f2-4278-47f4-916a-f4d71e386bc7

---

## Next Steps

1. Explore the Complete UGUI Demo.
2. Build your first production integration using the patterns above.

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
  * Explore the Complete UGUI Demo along with the Legacy Input, Unity Input System, and Runtime Monitor samples.

### Integration

* 🔄 **[Handling Scene Changes](Documentation/Guides/HandlingSceneChanges.md)**
  * Learn how to manage navigation history across scene reloads, and scene transitions.

### Troubleshooting

* 🛠️ **[Troubleshooting Guide](Documentation/Guides/Troubleshooting.md)**
  * Resolve common setup and integration issues, and verify that your Trailback configuration is working correctly.
