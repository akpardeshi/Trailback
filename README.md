# Trailback

> [!WARNING]
>
> ## Trailback v1.0.0-alpha
>
> Trailback is currently in alpha and undergoing community testing before the first stable release.
>
> **Current Status**
>
> * Core framework complete
> * Public API largely stable
> * Documentation actively expanding
> * Editor tooling and validation improving
>
> While the framework has been tested across a range of navigation scenarios, bugs and edge cases may still exist.
>
> Feedback, bug reports, and integration experiences are greatly appreciated:
>
> https://github.com/akpardeshi/Trailback/issues
>
> Thanks for helping make Trailback better.
>
> ---

A navigation framework for Unity that manages back navigation across screens, and popups using category-based history, navigation priorities, navigation blockers, and runtime diagnostics.

---

## Table of Contents

1. [What Is Trailback?](#what-is-trailback)
2. [Why I Built It](#why-i-built-it)

### Getting Started

3. [Installing Trailback](#installing-trailback)
4. [Choosing A Back Input Source](#choosing-a-back-input-source)
5. [Importing The Demo Sample](#step-2--import-the-demo-sample)
6. [Trailback Demo Scene](#trailback-demo-scene)

### Learning Trailback

7. [Understanding The Navigation Flow](#understanding-the-navigation-flow)
8. [Creating Trailback Components](#creating-trailback-components) 
9. [Quick Setup Validation](#quick-setup-validation) 
10. [Quick Start — Build Your First Trailback Integration](#quick-start--build-your-first-trailback-integration)

### Input & Events

11. [Connecting Back Input](#connecting-back-input)
12. [Event Subscription Reference](#event-subscription-reference)
13. [Navigation Root Reached](#navigation-root-reached)

### Navigation Features

14. [Adding Popups And Priorities](#adding-popups-and-priorities)
15. [Adding Navigation Blockers](#adding-navigation-blockers)
16. [Runtime Monitor](#runtime-monitor)

### Reference

17. [Requirements](#requirements)
18. [Next Steps](#next-steps)

---

## What Is Trailback?

Most projects begin with a simple back button implementation:

```csharp
Back();
```

As projects grow, navigation becomes more complicated:

* Popups should close before screens
* Different UI layers require different priorities
* Android back button support should remain consistent
* Debugging navigation state becomes difficult

Trailback provides a structured solution by separating:

* Navigation History
* Navigation Resolution
* Navigation Execution
* Input Handling
* Runtime Diagnostics

**Core features:**

* Category-based navigation history
* Navigation priorities
* Navigation blockers
* Input abstraction (Legacy Input Manager and Unity Input System)
* Runtime monitoring
* Setup validation tooling
* Flexible integration through a bridge abstraction

Trailback is not responsible for showing or hiding UI. Trailback determines:

```text
What should happen next?
```

Your application determines:

```text
How should it happen?
```

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

### Choosing A Back Input Source

The Trailback Demo is configured with a `LegacyBackInputSource` by default.

For most users, the demo should work immediately after importing the sample.

If your project is using the Legacy Input Manager:

```text
Edit
    → Project Settings
        → Player
            → Active Input Handling
                → Input Manager (Old)
```

No additional setup is required.

---

If your project is using the Unity Input System:

```text
Edit
    → Project Settings
        → Player
            → Active Input Handling
                → Input System Package (New)
```

replace the demo's `LegacyBackInputSource` with an `InputSystemBackInputSource`.

Refer to **Connecting Back Input** for step-by-step setup instructions.

A typical migration looks like this:

```text
Remove LegacyBackInputSource
        ↓
Create Input System Source
        ↓
Assign Input Action Reference
        ↓
Connect Back Requested Event
        ↓
Test Navigation
```

---

If your project uses both input systems, either input source can be used.

---

### Legacy Input Manager Warning

Some Unity versions display warnings that encourage migration away from the Legacy Input Manager.

These warnings are generated by Unity and do not indicate a problem with Trailback.

Trailback supports both the Legacy Input Manager and the Unity Input System, allowing you to use whichever input workflow is already established in your project.

---

### Step 2 — Import The Demo Sample

After installation, select the Trailback package inside Package Manager.

Open the **Samples** section.

Import:

```text
Trailback Demo
```

The demo sample contains:

* Example Screens
* Example Popups
* Navigation Categories
* Navigation Blockers
* Runtime Monitor
* Input Integration Examples
* Event Subscription Examples
* Reference Implementation Scripts

https://github.com/user-attachments/assets/cc10de26-4beb-4e00-92aa-352e9ee734bd

The demo is strongly recommended for first-time users and alpha testers.

---

### Step 3 — Open The Demo Scene

After importing the sample:

```text
Assets
    → Samples
        → Trailback
            → [1.0.0-alpha]
                → Trailback Demo
```

Open the demo scene and press Play.

The demo scene serves as both:

* Feature Showcase
* Reference Implementation

and is the fastest way to learn how Trailback is intended to be configured and integrated.

---

## Trailback Demo Scene

The package includes a fully functional demo scene that serves as both a feature showcase and a practical reference for integrating Trailback into your own project.

If you're new to Trailback, the demo scene is the best place to start. It allows you to explore the framework in a working environment and see how different navigation features behave during runtime.

### What You'll Learn

The demo scene covers:

* Navigation History
* Category Priorities
* Navigation Blockers
* Root Protection
* Root Reached Events
* Runtime Monitor
* Legacy Input Integration
* Unity Input System Integration
* Event Subscription Workflows
* Bridge-Based Integration

### What You'll See

The demo includes working examples of:

* Screens
* Popups
* Navigation Categories
* Navigation Handlers
* Back Input Sources
* Runtime Diagnostics

This makes it easy to observe how Trailback responds to navigation requests in different situations and how multiple navigation features interact with one another.

### What You'll Learn From The Code

The included scripts provide practical examples of:

* Implementing `IBackNavigable`
* Implementing `IBackNavigationBlocker`
* Creating Navigation Categories
* Creating Navigation Handlers
* Connecting Back Input Sources
* Creating a Trailback Integration Bridge
* Responding to Root Reached Events

### Recommended Learning Order

```text
Open Demo Scene
        ↓
Press Play
        ↓
Explore Navigation Flow
        ↓
Observe Runtime Monitor
        ↓
Review Demo Scripts
        ↓
Build Your Own Integration
```

The demo scene is intended to be explored, modified, and used as a reference. Spending a few minutes with it will usually provide a much faster understanding of Trailback than reading documentation alone.

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
Trailback
        ↓
Navigation Handler
        ↓
Hide Current UI / Show Previous UI
```

Trailback itself never reads keyboard, gamepad, touch, or mobile input directly. Input is always supplied through a `BackInputSource`.

---

## Creating Trailback Components

Right-click inside the Hierarchy:

```text
Trailback
├── Create Legacy Input Source
├── Create Input System Source
├── Create Event Listener
├── Create Runtime Monitor
└── Create Complete Setup
```

Recommended for first-time setup:

```text
Trailback
    → Create Complete Setup
```

This creates:

```text
Trailback
│
├── Legacy Back Input Source
├── Trailback Event Listener
└── Runtime Monitor
```

<img width="359" height="442" alt="TrailbackContextMenu_FinalCut" src="https://github.com/user-attachments/assets/b832fdad-17e4-4e1a-a01f-c42feee3de86" />

If your project uses the Unity Input System instead of the Legacy Input Manager, see [Connecting Back Input](#connecting-back-input) below before continuing.

---

## Quick Setup Validation

Trailback includes a lightweight setup validation utility that performs a quick health check of common Trailback components.

Checks include:
* Missing Components
* Duplicate Components
* Disabled Components

The Quick Validator is intended to catch common setup mistakes before runtime.

Open:

```text
Tools
    → Trailback
        → Quick Validate Setup
```

The validator scans the current scene and prints a report to the Unity Console.

**Checks performed:**

| Category | Checks |
|---|---|
| EventSystem | Presence, Duplicate or disabled EventSystems |
| Back Input Sources | Presence, Duplicate or disabled `BackInputSource` components |
| Trailback Event Listeners | Presence, Duplicate or disabled listeners |
| Runtime Monitor | Presence, duplicates, disabled state |

**Severity levels:**

* **Info** — general information (e.g. `Runtime Monitor found.`)
* **Warning** — configuration issues worth reviewing (e.g. `Multiple BackInputSources found (2).`)
* **Error** — critical problems that may prevent Trailback from functioning correctly

Validation messages include clickable scene object references — selecting a message highlights the related object in the Hierarchy.

**Recommended workflow:**

```text
Install Trailback
    ↓
Create Setup
    ↓
Validate Setup
    ↓
Fix Issues
    ↓
Begin Integration
```

Run validation immediately after setup, and again after any scene migration or refactor — it's the fastest way to catch a misconfigured or disabled component before it costs you a debugging session.

https://github.com/user-attachments/assets/7893216b-4d1b-46ef-bcad-e1a4b3278240

---

## Quick Start — Build Your First Trailback Integration

This walkthrough builds a small but complete Trailback setup:

```text
2 Screens
2 Popups (one of which blocks back navigation)
1 Navigation Handler
1 Bridge
1 Navigation Controller
```

By the end, you'll be able to open screens and popups, press Back to return through history correctly, and confirm that a blocked popup stops navigation until dismissed.

### Step 1 — Create The Trailback Setup

Run Create Complete Setup as shown in [Creating Trailback Components](#creating-trailback-components)

### Step 2 — Create Navigation Categories

Create two category assets:

```text
Create
    → Trailback
        → Navigation Category
```

| Category | Priority |
|---|---|
| `UI` | 0 |
| `Popup` | 100 |

Higher priority categories resolve first:

```text
Popup   → Resolved First
UI      → Resolved Second
```

### Step 3 — Create A Reusable Base UI Class

```csharp
using UnityEngine;
using YourNamespace.Trailback; // replace with the actual Trailback namespace

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

> `UIBase` is a reusable base implementation shared by screens, popups, and any other navigable UI element. This implementation is intentionally simplified for learning — the demo scene included with Trailback contains a more complete, production-ready version.

Every subclass must have its `NavigationCategory` assigned through the Inspector.

### Step 4 — Create Screens

```csharp
public sealed class HomeScreen : UIBase { }
```

```csharp
public sealed class SettingsScreen : UIBase { }
```

Assign the **UI** category to both.

### Step 5 — Create Popups

```csharp
public sealed class InfoPopup : UIBase { }
```

A popup that blocks back navigation until explicitly dismissed:

```csharp
public sealed class LockedPopup : UIBase, IBackNavigationBlocker
{
    [field: SerializeField]
    public BackNavigationMode BackNavigationMode { get; private set; } = BackNavigationMode.Block;
}
```

Assign the **Popup** category to both.

**To make a popup block navigation, two things must both be true:**

1. It implements `IBackNavigationBlocker`.
2. `BackNavigationMode` is set to `Block` in the Inspector.

If either is missing, the popup will not block navigation.

### Step 6 — Create A Navigation Handler

The handler decides *how* navigation executes once Trailback has resolved *what* should happen next.

```csharp
public sealed class DemoBackNavigationHandler : IBackNavigationHandler
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

### Step 7 — Create A Bridge

Applications should communicate with Trailback through a bridge rather than referencing Trailback directly — this keeps a single integration point if the framework's internals ever need to change.

```csharp
using System;

public sealed class DemoTrailbackBridge : TrailbackIntegrationBridge
{
    public override event Action RootReached
    {
        add => Trailback.OnNavigationRootReached += value;
        remove => Trailback.OnNavigationRootReached -= value;
    }

    public override void Show(IBackNavigable element) => Trailback.ReportShown(element);

    public override void Hide(IBackNavigable element) => Trailback.ReportHidden(element);

    public override bool Back() => Trailback.Back();

    public override void ResetHistory() => Trailback.ResetHistory();
}
```

### Step 8 — Create A Navigation Controller

This ties everything together: it owns the bridge, registers the handler, and reports visibility changes as screens open and close.

```csharp
using UnityEngine;

public sealed class DemoNavigationController : MonoBehaviour
{
    private DemoTrailbackBridge _bridge;

    [SerializeField] private HomeScreen homeScreen;
    [SerializeField] private SettingsScreen settingsScreen;

    private void Awake()
    {
        _bridge = new DemoTrailbackBridge();
        Trailback.SetNavigationHandler(new DemoBackNavigationHandler());
    }

    private void Start()
    {
        homeScreen.Show();
        _bridge.Show(homeScreen);
    }

    public void OpenSettings()
    {
        homeScreen.Hide();
        _bridge.Hide(homeScreen);

        settingsScreen.Show();
        _bridge.Show(settingsScreen);
    }

    public void HandleBackRequested()
    {
        _bridge.Back();
    }
}
```

### Step 9 — Connect Back Input

This is the step most users miss. Creating an input source only **generates** back requests — it does not automatically handle them. Without this step, pressing Back will do nothing, even though everything else is configured correctly.

**If using the Legacy Input Manager** (created automatically by Complete Setup):

1. Select the `LegacyBackInputSource` object in the Hierarchy.
2. In the Inspector, find its **Back Requested** event.
3. Drag in your `DemoNavigationController` and assign `HandleBackRequested()` as the callback.

https://github.com/user-attachments/assets/395cba13-5e51-4693-a0e1-d6af7cb85d8b

**If using the Unity Input System:**

1. Remove the generated `LegacyBackInputSource`.
2. Create `Trailback → Create Input System Source`.
3. Create an Input Action (e.g. `UI/Cancel`) with bindings for Escape, Gamepad B/Circle, and the Android back button.
4. Assign the Input Action Reference to `InputSystemBackInputSource`.
5. Wire its **Back Requested** event to `HandleBackRequested()`, exactly as in the Legacy steps above.

https://github.com/user-attachments/assets/4bee9fb1-17bc-4cbd-88c8-cce4401063d9

Either way, the resulting flow is the same:

```text
Back Input
        ↓
BackInputSource
        ↓
BackRequested Event
        ↓
DemoNavigationController.HandleBackRequested()
        ↓
_bridge.Back()
        ↓
Trailback
```

### Step 10 — Test It

```text
Home Screen
        ↓
Open Settings
        ↓
Open Info Popup
        ↓
Press Back
        ↓
Info Popup closes, Settings Screen visible
        ↓
Press Back
        ↓
Home Screen visible
```

Then try opening a `LockedPopup` and pressing Back — navigation should stop until you dismiss it explicitly. Open the Runtime Monitor (see below) to watch this happen in real time.

You now have a working Trailback integration.

---

## Connecting Back Input

> See [Step 9](#step-9--connect-back-input) above for the concrete wiring steps. This section explains *why* the step exists.

A `BackInputSource`'s only job is to detect input and raise a `BackRequested` event. It is **not** responsible for executing navigation, managing history, or showing/hiding UI — that separation is intentional, and it's why the wiring step can't be skipped.

```text
BackInputSource is responsible for:
  - Detecting back input
  - Raising BackRequested events

BackInputSource is NOT responsible for:
  - Executing navigation
  - Managing navigation history
  - Showing or hiding UI
```

This keeps Trailback fully decoupled from any specific input system. Custom input providers (e.g. a VR controller button, a custom touch gesture) can be added by raising the same event.

---

## Event Subscription Reference

Trailback supports more than one way to subscribe to its events, so you can pick the workflow that fits your team.

| Event | Workflow | Recommended For |
|---|---|---|
| `BackRequested` | Inspector (UnityEvent) | Designers, rapid prototyping |
| `BackRequested` | Code (`inputSource.BackRequested += ...`) | Programmers, production projects — used throughout this guide |
| `OnNavigationRootReached` | Inspector via `TrailbackEventListener` | Designers, UI/audio reactions |
| `OnNavigationRootReached` | Bridge (`_bridge.RootReached += ...`) | **Recommended.** Keeps application code decoupled from the framework. |
| `OnNavigationRootReached` | Direct (`Trailback.OnNavigationRootReached += ...`) | Fully supported, but creates a direct dependency on Trailback. Prefer the bridge for production code. |

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

**Inspector subscription:** assign a callback to the `BackInputSource`'s exposed event field directly in the Inspector — no code required. This is the same mechanism used in [Step 9](#step-9--connect-back-input).

**Root Reached, via Inspector:** add a `TrailbackEventListener` component to any GameObject and assign UnityEvent callbacks — useful for playing a sound or showing an exit confirmation without writing code.

---

## Navigation Root Reached

Trailback raises an event when navigation reaches the root of the history stack and no further back navigation can be performed.

```text
Main Menu
    ↓
Back
    ↓
Navigation Root Reached
```

**Common uses:**

* Exit Confirmation Dialogs
* Return To Main Menu
* Application Quit Logic
* Analytics Events
* Audio Feedback

Trailback supports three ways to respond to this event.

### Inspector Workflow

Recommended for: designers, UI configuration, visual workflows.

Create a Trailback Event Listener using either approach:
> Option 1 (Recommended)
> ```text
> GameObject
>     → Trailback
>         → Create Event Listener
>```

> Option 2
> ```text
> Add a `TrailbackEventListener` component manually to a GameObject, then assign callbacks through the Inspector.
> ```

```text
On Navigation Root Reached
    ↓
Show Exit Confirmation Popup
```

```text
On Navigation Root Reached
    ↓
Play UI Sound
```

Flow:

```text
Trailback
    ↓
OnNavigationRootReached
    ↓
TrailbackEventListener
    ↓
UnityEvent
    ↓
Inspector Callback
```

This workflow lets designers react to navigation events without writing code.

### Bridge Workflow (Recommended)

Recommended for: application code, production projects, clean architecture.

Subscribe through your bridge implementation:

```csharp
private void OnEnable()
{
    _bridge.RootReached += HandleRootReached;
}

private void OnDisable()
{
    _bridge.RootReached -= HandleRootReached;
}

private void HandleRootReached()
{
    exitConfirmationPopup.Show();
}
```

This is the preferred integration approach because application systems remain isolated from the Trailback framework implementation.

### Direct Framework Subscription

Fully supported, but introduces a direct dependency on Trailback:

```csharp
private void OnEnable()
{
    Trailback.OnNavigationRootReached += HandleRootReached;
}

private void OnDisable()
{
    Trailback.OnNavigationRootReached -= HandleRootReached;
}

private void HandleRootReached()
{
    Debug.Log("Navigation root reached.");
}
```

```text
Trailback
    ↓
Application Code
```

For most applications, the Bridge Workflow remains the recommended integration path.

---

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

Navigation will not continue until the blocking element is dismissed by the user — see [Step 5](#step-5--create-popups) for the full requirements.

https://github.com/user-attachments/assets/35a5a1f2-4278-47f4-916a-f4d71e386bc7

---

## Runtime Monitor

Create a Runtime Monitor:

```text
GameObject
    → Trailback
        → Create Runtime Monitor
```

<img width="1920" height="1080" alt="Creating Runtime Monitor" src="https://github.com/user-attachments/assets/4fb77aec-bfaf-4805-a9cd-6d48a45af19b" />

The Runtime Monitor provides real-time visibility into Trailback's navigation state.

**Displayed information includes:**

* Current Navigation Entry
* Previous Navigation Entry
* Navigation Availability
* History Statistics
* Navigation Block Reason
* Navigation Block Details

Example:

```text
TRAILBACK STATE

Current: Locked Popup
Previous: InfoPopup
Can Go Back: False

Reason: BlockedByConfiguration
Details: Locked Popup

HISTORY STATS

Categories: 2
Total Entries: 3
Highest Category: Popup
```

### Runtime Visibility Toggle

The Runtime Monitor includes built-in visibility control. Developers can choose whether the monitor should be visible when the scene starts.

**Inspector:** `Start Visible`

* Enabled → Monitor Visible At Startup
* Disabled → Monitor Hidden At Startup

This allows development, QA, demonstration, and testing environments to choose the most appropriate default behavior.

### Runtime Toggle Button

The monitor can also be shown or hidden while the application is running.

Typical workflow:

```text
Debug ON
    ↓
Button Press
    ↓
Monitor Hidden
    ↓
Debug OFF

Debug OFF
    ↓
Button Press
    ↓
Monitor Visible
    ↓
Debug ON
```

This allows developers and testers to keep diagnostic information available without permanently occupying screen space.

https://github.com/user-attachments/assets/4ee38364-ab7f-48ee-bc5e-b9b740c44e72

### Why Use The Runtime Monitor?

Useful for:

* Verifying Navigation History
* Debugging Category Priorities
* Testing Navigation Blockers
* Inspecting Runtime State
* Validating Navigation Flow
* Demonstrating Framework Behavior

https://github.com/user-attachments/assets/05a8c5b9-e36c-4feb-91d5-922e388cc850

The Runtime Monitor is intended for development and debugging workflows — disable it by default in shipping builds unless your application intentionally exposes diagnostic information to end users.

---

## Requirements

* Unity 2022.3 LTS or newer
* Legacy Input Manager **or** Unity Input System
* TextMeshPro (optional — required only for the Runtime Monitor)

---

## Next Steps

1. Explore the Demo Scene.
2. Build your first production integration using the patterns above.

