# Table of Contents

1. [Introduction](#introduction)
2. [Core Philosophy](#core-philosophy)
3. [Core Navigation](#core-navigation)
    * [Navigation History](#navigation-history)
    * [Navigation Categories](#navigation-categories)
    * [Navigation Blocking](#navigation-Blocking)
    * [Root Protection](#root-protection)
    * [OnNavigationRootReached](#onnavigationrootreached)
    * [Scene Reload Support](#scene-reload-support)
    * [UI Framework Agnostic](#ui-framework-agnostic)
4. [Framework Integration](#framework-integration)
    * [Input Abstraction](#input-abstraction)
    * [Bridge-Based Integration](#bridge-based-integration)
5. [Next Steps](#next-steps)


# Introduction

Trailback focuses on one thing: managing back navigation in Unity applications.

Instead of handling screen history, layered UI, and back button behavior separately, Trailback brings those responsibilities together into a single navigation system. The sections below highlight the core features available in the framework.

```text
                    Press Back
                         ↓
             Is Navigation Blocked?
                  ┌──────┴──────┐
                Yes             No
                 ↓              ↓
          Stop Navigation    Resolve  Highest Priority
                               ↓
                    Previous Navigation Entry
                               ↓
                      Root Protected?
                  ┌──────┴──────┐
                Yes             No
                 ↓              ↓
   OnNavigationRootReached   Navigate Back
```

---

## Core Philosophy

Trailback keeps the core framework focused on one responsibility: back navigation.

Features that require on additional Unity packages, or are better presented as implementation examples, are included separately as reference samples instead of being built into the core package.

This keeps the core framework lightweight and free from unnecessary dependencies, while letting you include only the integrations your project actually needs.

Reference implementations for the Legacy Input Manager, Unity Input System, and Runtime Monitor are available in the **Reference Samples Guide**.

---

# Core Navigation

## Navigation History

```text
Home Screen
      ↓
Settings Screen
      ↓
Info Popup
      ↓
Press Back
      ↓
Info Popup Closes
      ↓
Settings Screen
      ↓
Press Back
      ↓
Home Screen
```

**Why it exists**

Keeping track of previously opened screens and popups becomes increasingly difficult as an application's UI grows.

**How Trailback helps**

Trailback automatically records navigation history and restores the previous navigation target whenever the user navigates back.

**Common uses**

* Screen navigation
* Popup navigation
* Multi-layer UI
* Mobile applications

---

## Navigation Categories

```text
Navigation Categories

Screen (Priority 0)

Popup (Priority 100)  
        ↓
Press Back
        ↓
Popup closes
        ↓
 Press Back
        ↓
Screen becomes visible
```

**Why it exists**

When several UI layers are visible at the same time, deciding which one should respond to the Back button often leads to custom conditional logic.

**How Trailback helps**

Assign priorities to navigation categories to define the order in which Trailback resolves back navigation. When Back is pressed, Trailback always resolves the highest-priority active category first.

Example:

```text
Popup (Priority 100)

        ↓

Screen (Priority 10)
```

The popup is resolved before the screen.

---

## Navigation Blocking

Navigation stays blocked until your application decides the current workflow is complete. This usually means closing the popup and reporting it as hidden.

```text
Purchase Confirmation
        ↓
 Press Back
        ↓
 Navigation Blocked
        ↓
User selects Cancel
        ↓
Popup closes
        ↓
 Press Back
        ↓
Previous Screen
```

**Why it exists**

Some parts of the UI should temporarily prevent back navigation until the user completes or dismisses the current workflow.

**How Trailback helps**

Implement `IBackNavigationBlocker` to temporarily block navigation while your application remains in control of when navigation can continue.

**Common uses**

* Purchase confirmation
* Terms of Service
* Unsaved changes
* Critical warnings

---

## Root Protection

```text
Home Screen
      ↓
Settings
      ↓
Info Popup
      ↓
Back
      ↓
Settings
      ↓
Back
      ↓
Home Screen
      ↓
Back
      ↓
OnNavigationRootReached
```

**Why it's useful**

Most applications have a screen that acts as the starting point for navigation, such as a Home Screen or Main Menu. Once the user reaches that screen, pressing **Back** usually shouldn't remove it from the navigation history.

**How it works**

Enable **Protect Root** on the navigation category that contains your application's primary screens. When Back reaches the last entry in that category, Trailback raises the **OnNavigationRootReached** event instead of navigating any further, allowing your application to decide what happens next.

**Best Practices**

✔ Enable **Protect Root** on a single navigation category.

✔ Use that category for the screens that form the root of your application's navigation, such as the Home Screen or Main Menu.

✔ Enable **Protect Root** on only one navigation category.

That category should represent the root of your application's navigation flow. Protecting multiple categories can lead to unexpected **OnNavigationRootReached** events and make Back navigation more difficult to follow.

**Typical use cases**

* Main Menu
* Home Screen
* Dashboard
* Application Launcher

---

## OnNavigationRootReached

**Why it exists**

Eventually the user reaches the beginning of the navigation history. At that point, applications often need to perform their own action instead of navigating further.


```text
Home Screen
      ↓
Press Back
      ↓
OnNavigationRootReached
      ↓
Your Application
Chooses What Happens
      │
 ┌────┼─────────┐
 ↓    ↓         ↓
Exit  Show     Ignore
App   Popup    Event
```

**How Trailback helps**

Trailback raises the **OnNavigationRootReached** event, giving your application a chance to decide what happens next.

**Common uses**

* Exit confirmation
* Quit application
* Return to launcher
* Return to the main menu
* Analytics

---

# Framework Integration

Trailback is designed to fit into existing Unity projects rather than dictate how they're structured.

It communicates through a small set of abstractions instead of depending on a specific input framework or application architecture, making it easy to integrate into both new and existing projects.

---

## Input Abstraction

**Overview**

Every project handles input a little differently. Some rely on Unity's Legacy Input Manager, others use the Unity Input System, and many have their own input layer.

Rather than depending on any one solution, Trailback receives back navigation requests through `BackInputSource`, an abstract `MonoBehaviour` that you extend to detect back input and raise the `BackRequested` event.

Each input implementation is responsible for detecting user input and raising the `BackRequested` event. From that point on, the navigation flow is exactly the same, regardless of where the input originated.

```text
Keyboard
Gamepad
Touch
Android Back
Custom Input
        ↓
BackInputSource
        ↓
BackRequested Event
        ↓
Navigation Controller
```

**Benefits**

* No dependency on a specific input framework
* Easy to integrate with existing projects
* Supports custom input implementations
* Consistent back navigation across platforms

> [!TIP]
>
> Trailback includes reference samples for both Unity's Legacy Input Manager and the Unity Input System. See the **Reference Samples Guide** for complete integration examples.

---

## Bridge-Based Integration

**Overview**

As a project grows, it's easy for framework-specific calls to end up scattered throughout the codebase. Over time, that makes navigation harder to follow and more difficult to change.

`TrailbackIntegrationBridge` keeps those interactions in one place. Your Navigation Controller talks to the bridge, and the bridge forwards requests to `TrailbackFacade`. The rest of your application doesn't need to know anything about Trailback's internal API.

When your application starts, initialize Trailback before registering the navigation handler or showing the first screen.

```text
Application Startup
        ↓
Create Integration Bridge
        ↓
InitializeSession()
        ↓
Register Navigation Handler
        ↓
Show Initial Screen
```

Once initialization is complete, the navigation flow looks like this:

```text
BackInputSource
        ↓
BackRequested Event
        ↓
Navigation Controller
        ↓
TrailbackIntegrationBridge
        ↓
TrailbackFacade
        ↓
Navigation Handler
```

**Why Use a Bridge?**

Using a bridge keeps your application code separate from the framework.

Instead of calling `TrailbackFacade` throughout your project, the rest of the application communicates with the bridge. If your navigation architecture changes later—or you decide to replace or extend the integration—you only need to update one place.

**Benefits**

* Keeps Trailback-specific code in one place
* Reduces coupling between your application and the framework
* Makes the navigation flow easier to follow
* Simplifies maintenance and testing
* Makes future changes easier to manage

---

## Scene Reload Support

**Overview**

Scene changes don't always mean your navigation should start over.

Some projects replace the entire UI when loading a new scene, while others keep it alive using additive scenes, persistent managers, or Addressables. Because every project handles this differently, Trailback doesn't automatically reset the navigation history.

Instead, your application decides whether a scene change starts a new navigation flow or continues the current one.

```text
Scene Changed
      ↓
Did the Navigation Flow Change?
      │
 ┌────┴────┐
 ↓         ↓
Yes        No
 ↓          ↓
Reset     Keep
History   History
```

**Why It Works This Way**

By leaving this decision to your application, Trailback can support a wide range of scene management workflows without making assumptions about how your project is structured.

This works well for:

* Single-scene projects
* Multi-scene projects
* Persistent UI
* Addressables
* Custom loading workflows

For examples and implementation details, see the [**Handling Scene Changes**](HandlingSceneChanges.md).

---

## UI Framework Agnostic

**Overview**

Trailback doesn't manage your UI—it manages your navigation history.

Showing screens, hiding popups, playing transitions, and updating the interface all remain the responsibility of your application. Trailback simply tracks navigation and determines where Back should go next.

As your UI changes, report those changes through the integration bridge to keep the navigation history in sync.

```text
Your UI Framework
        ↓
Navigation Controller
        ↓
TrailbackIntegrationBridge
        ↓
Trailback
```

**Why It Works This Way**

By staying out of UI rendering and presentation, Trailback can fit into almost any Unity project without requiring a particular UI framework or architecture.

Whether you're using:

* UGUI
* UI Toolkit
* A custom UI framework
* An existing project architecture

the integration stays the same.

Trailback keeps track of navigation history and resolves Back navigation. Your application remains responsible for creating, showing, hiding, transitioning, and destroying UI.

---

# Next Steps

Continue with the guide that best matches what you're working on.

| Guide                                                  | Description                                                                                         |
|--------------------------------------------------------| --------------------------------------------------------------------------------------------------- |
| [**README**](../../README.md#table-of-contents)    | Return to the main documentation for installation, project requirements, and the Quick Start guide. |
| [**Reference Samples Guide**](ReferenceSamplesGuide.md) | Explore the included reference implementations and learn from complete integration examples.                  |
| [**Handling Scene Changes**](HandlingSceneChanges.md)  | Learn how to manage navigation history across scene changes and multi-scene workflows.              |
| [**Troubleshooting**](Troubleshooting.md)              | Resolve common setup, configuration, and integration issues.                                        |
