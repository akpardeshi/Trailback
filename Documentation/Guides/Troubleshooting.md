# Troubleshooting

## Table of Contents
1. [Before You Begin](#before-you-begin) 
2. [Pressing Back Does Nothing](#pressing-back-does-nothing) 
3. [Back Navigation Executes Twice](#back-navigation-executes-twice) 
4. [Navigation History Is Incorrect](#navigation-history-is-incorrect)
5. [Trailback Starts With Stale History](#trailback-starts-with-stale-history)
6. [Back Doesn't Return To The Previous Screen](#back-doesnt-return-to-the-previous-screen)
7. [Locked Popup Never Closes](#locked-popup-never-closes)
8. [OnNavigationRootReached Never Fires](#onnavigationrootreached-never-fires)
9. [Root Screen Disappears When Pressing Back](#root-screen-disappears-when-pressing-back)
10. [OnNavigationRootReached Fires Too Early](#onnavigationrootreached-fires-too-early)
11. [Scene Reload Causes Missing References](#scene-reload-causes-missing-references)
12. [Still Having Problems?](#still-having-problems)
13. [Report an Issue](#report-an-issue)

## Before You Begin

Most Trailback integration issues come down to a few common setup mistakes.

Before diving into the troubleshooting sections, check the following:

* Back input is reaching Trailback.
* Screens and popups are being reported correctly.
* Navigation history has been reset when starting a new navigation flow.

If you're using one of the included reference samples, it's also worth comparing your setup with the **Complete UGUI Demo**. It's often the quickest way to spot configuration differences before making changes to your own project.

---

## Pressing Back Does Nothing

If pressing **Escape**, the **Android Back** button, or your configured back input has no effect, check the following:

Expected flow:

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
TrailbackFacade.Back()
      ↓
Navigation Handler
```
Work through each stage of the navigation pipeline from top to bottom. Once the flow stops, you've usually found the source of the problem.

### 1. Verify the Input Source

First, confirm that your input is reaching Trailback.

Make sure:

* A `BackInputSource` exists in the scene.
* The component is enabled.
* Your configured back input is being detected.
* Pressing the input raises the `BackRequested` event.

### 2. Verify the Navigation Controller

If the input is working, the next step is to check your navigation controller.

Confirm that:

* `BackRequested` is subscribed exactly once.
* `HandleBackRequested()` is being called.
* `HandleBackRequested()` forwards the request to the integration bridge.

### 3. Verify the Integration Bridge

Once the navigation controller receives the event, verify that the request reaches the bridge.

Check that:

* `_bridge.Back()` is being called.
* The correct `TrailbackIntegrationBridge` instance is being used.

### 4. Verify Trailback

Finally, confirm that Trailback has everything it needs to perform back navigation.

Make sure:

* A navigation handler has been registered.
* Screens and popups have been reported correctly.
* The navigation history contains valid entries.
* * The Navigation Controller has registered the navigation handler during initialization.

---

## Back Navigation Executes Twice

If a single Back press closes multiple screens or skips entries in the navigation history, the Back event is usually being handled more than once.

A common cause is subscribing to the same BackRequested event in both code and the Inspector.

For example:

* Subscribe to `BackRequested` in `OnEnable()`.
* Also assign `HandleBackRequested()` to the **Back Requested** UnityEvent in the Inspector.

That causes a single Back press to invoke `HandleBackRequested()` twice.

```text
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

Pick one approach:

* Code subscription (recommended for production)
* Inspector subscription

Using both at the same time causes duplicate back navigation requests.

---

## Navigation History Is Incorrect

Trailback can only restore navigation elements that have been reported to it. If the navigation history no longer matches what's actually visible, Back navigation won't behave as expected.

Common symptoms include:

* Back returns to the wrong screen.
* Back skips one or more screens.
* Back returns to a screen that's already been closed.
* Back stops working after several navigation operations.

In most cases, the cause is that a screen or popup wasn't reported to Trailback when its visibility changed.

> [!TIP]
>
> If you're using the **Runtime Monitor** reference sample, compare Trailback's current navigation history with what you expect to see on screen. Mismatches usually indicate that a screen or popup wasn't reported correctly when its visibility changed.

### Showing UI

Whenever a screen or popup becomes visible, register it with Trailback.

```csharp
settingsScreen.Show();
_bridge.Show(settingsScreen);
```

### Hiding UI Permanently

Before permanently removing a screen or popup from the navigation flow, report it as hidden.

```csharp
settingsScreen.Hide();
_bridge.Hide(settingsScreen);
```

### Destroying Navigation Elements

If an object is destroyed without first notifying Trailback, the navigation history may still contain a reference to it.

Correct:

```csharp
_bridge.Hide(settingsScreen);

Destroy(settingsScreen.gameObject);
```

### Avoid Duplicate Registration

Only register a screen or popup when its visibility actually changes.

Calling `Show()` multiple times without a matching `Hide()` may register the same navigation element more than once, depending on your configured duplicate policy. This can cause Back navigation to revisit the same screen multiple times or produce navigation behavior you didn't expect.

> [!NOTE]
>
> Trailback relies on your application to report visibility changes. Whenever a screen or popup is shown, hidden, or destroyed, make sure Trailback is notified as well. Keeping those calls in sync with your UI ensures the navigation history stays accurate.

---

## Trailback Starts With Stale History

If Back navigation behaves unexpectedly as soon as your application starts, or Trailback seems to remember navigation entries from a previous session, `InitializeSession()` was probably skipped or called too late.

This usually happens when the first screen is registered before the framework has been initialized.

Check the following:

* `InitializeSession()` is called exactly once during application startup.
* `InitializeSession()` is called before registering the navigation handler.
* `InitializeSession()` is called before the first `Show()` or `ReportShown()` call.
* The first screen or popup isn't registered until after the framework has been initialized.

The startup sequence should look like this:

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

> [!NOTE]
>
> `InitializeSession()` prepares Trailback for a new application session and is normally called once during startup.
>
> During normal application flow, use `ResetHistory()` only when your application intentionally begins a new navigation flow—for example, after loading a new scene or returning to the main menu.

## Back Doesn't Return To The Previous Screen

If pressing **Back** doesn't return to the previous screen, the navigation history is usually out of sync with your UI.

In most cases, this happens because a screen was removed from the navigation history even though you expected to navigate back to it.

### Expected Navigation Flow

When moving between screens, the current screen is usually hidden, but it remains in the navigation history.

```text
Current UI

Screen A
    │ Hide
    ↓
Screen B


Navigation History

Screen A
    ↓
Screen B
    │ Back
    ↓
Screen A
```

Because **Screen A** is still in the history, Trailback can restore it when Back is pressed.

> [!NOTE]
>
> **Hidden** doesn't mean **removed**.
>
> In most navigation flows, a screen is hidden because another screen is temporarily covering it. It remains in the navigation history so Trailback can restore it when Back is pressed.
>
> A screen should only be removed from the navigation history when it's permanently leaving the current navigation flow.

### A Common Mistake

Calling `Hide()` and reporting the screen as hidden removes it from the navigation history.

```text
Current UI

Screen A
    │ Hide + ReportHidden()
    ↓
Screen B


Navigation History

Screen B
    │ Back
    ↓
(No Previous Screen)
```

Since **Screen A** is no longer in the history, there's nothing for Trailback to navigate back to.

### Correct

Only report a screen as hidden when it's leaving the current navigation flow permanently.

```csharp
screenA.Hide();

screenB.Show();
_bridge.Show(screenB);
```

### Avoid

If you expect to return to a screen later, don't remove it from the navigation history.

```csharp
screenA.Hide();
_bridge.Hide(screenA);

screenB.Show();
_bridge.Show(screenB);
```

> [!TIP]
>
> A simple rule to remember:
>
> * **Hide** a screen when you expect to return to it.
> * Call **ReportHidden** only when the screen is permanently leaving the current navigation flow.


## Locked Popup Never Closes

If the popup is intentionally blocking navigation, this behavior is expected.

If the Locked Popup stays open when Back is pressed, first check whether that's the expected behavior.

A popup implementing `IBackNavigationBlocker` intentionally blocks navigation while its `BackNavigationMode` is set to `Block`.

Close the popup through your own application logic before allowing navigation to continue.

---

## OnNavigationRootReached Never Fires


This event is only raised when Back navigation reaches the protected root of the active navigation category.

```text
Back
      ↓
History Root?
      │
 ┌────┴────┐
 ↓         ↓
No        Yes
 ↓         ↓          
Back   Protect Root?
            │
      ┌─────┴─────┐
      ↓           ↓
     No          Yes
      ↓           ↓
 Navigate    Raise Event
```

Check the following:

* The active navigation category has **Protect Root** enabled.
* The Home Screen (or your root screen) has been reported to Trailback.
* The navigation history is already at its root.
* The event is subscribed using one of the supported approaches:

    * `TrailbackIntegrationBridge`
    * `TrailbackEventListener`
    * Direct `TrailbackFacade` subscription

---

## Root Screen Disappears When Pressing Back

If pressing Back hides the Home Screen instead of raising **OnNavigationRootReached**, Root Protection is most likely disabled.

Check the navigation category assigned to your root screen.

```text
Navigation Category
        ↓
Protect Root ✓
```

Without Root Protection, Trailback treats the last history entry like any other and removes it.

---

## OnNavigationRootReached Fires Too Early

This usually happens when more than one navigation category has **Protect Root** enabled.

Trailback expects a single category to represent the root of your application's navigation.

Recommended configuration:

```text
Popup
Protect Root ✗

Overlay
Protect Root ✗

Screen
Protect Root ✓
```

Only one navigation category should have Root Protection enabled.

Otherwise you may see:

* `OnNavigationRootReached` event raised earlier than expected
* A different category blocking navigation
* Navigation stopping before returning to the expected screen

---

## Scene Reload Causes Missing References

When a new scene is loaded, Unity destroys the UI objects from the previous scene.

If the navigation history is preserved, it may still contain references to those destroyed objects.

```text
Scene Reload
      ↓
ResetHistory()
      ↓
Register New Root
```


Until automatic scene lifecycle management is available, clear the history before registering UI in the new scene.

Using the integration bridge (recommended):

```csharp
_bridge.ResetHistory();
```

Or call the framework directly:

```csharp
TrailbackFacade.ResetHistory();
```

---

# Still Having Problems?

If something isn't working as expected, try the following before opening an issue:

* Review the documentation for the component you're integrating.
* Review the Reference Samples Guide. 
* Review Handling Scene Changes.
* Work through the checkpoints in the Quick Start guide.
* Compare your setup with the included demo scene.

If you're still running into the same issue, please open an issue on GitHub. The more information you can provide, the easier it is to understand what's happening and reproduce the problem.

When reporting an issue, include:

* Unity version
* Trailback version
* Input system (Legacy Input Manager or Unity Input System)
* Steps to reproduce
* Console output or exception stack trace
* Screenshots or a short video, if applicable

## Report an Issue

If you've found a bug, run into an unexpected behavior, or spotted something that could be improved in the documentation, I'd love to hear about it. If possible, include a small reproduction project.

👉 **Report it on GitHub Issues:**
https://github.com/akpardeshi/Trailback/issues