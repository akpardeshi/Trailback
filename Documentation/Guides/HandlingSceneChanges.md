## Table of Contents

* [Handling Scene Changes](#handling-scene-changes)
* [Design Philosophy](#design-philosophy)
* [Why History Isn't Reset Automatically](#why-history-isnt-reset-automatically)
* [Resetting Navigation History](#resetting-navigation-history)
* [When Should I Reset History?](#when-should-i-reset-history)
* [Single Scene Workflow](#single-scene-workflow)
* [Additive Scene Workflow](#additive-scene-workflow)
* [Persistent UI Managers](#persistent-ui-managers)
* [Destroying Navigation Elements](#destroying-navigation-elements)
* [Best Practices](#best-practices)
* [Common Mistakes](#common-mistakes)

## Handling Scene Changes

Scene management looks different from one Unity project to another. Some projects replace the entire UI when loading a new scene, while others keep it alive using additive scenes or persistent managers.

Because of that, Trailback doesn't automatically reset navigation history when scenes change. Your application decides when a navigation flow starts and ends.

This guide covers when to reset the navigation history, when to keep it, and how to use Trailback across different scene management workflows.

## Design Philosophy

Rather than enforcing a particular scene management strategy, Trailback adapts to the one your project already uses.

Instead, it focuses on maintaining navigation history while leaving lifecycle decisions to the application. This makes the framework flexible enough to support a wide range of Unity projects, including:

* Single-scene projects
* Multi-scene workflows
* Additive scene loading
* Persistent UI
* Addressables
* Custom application architectures

---

## Why History Isn't Reset Automatically

At first glance, clearing the navigation history whenever a scene changes might seem like the obvious choice. In practice, though, that doesn't work for every project.

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

Consider a setup like this:

```text
Persistent UI Scene
        │
        ├── Home Screen
        ├── Settings
        └── Pause Menu

Gameplay Scene
        │
        └── Environment
```

Here, only the gameplay scene changes. The UI is still active, so the existing navigation history is still valid.

In other projects, loading a new scene replaces the entire UI. In that case, keeping the old navigation history would leave Trailback pointing to screens that no longer exist.

Because Trailback can't know which approach your project uses, it never resets the history automatically. Instead, you decide when a new navigation flow begins.

A **navigation flow** is a group of related screens and popups that share the same navigation history, such as a Main Menu, Gameplay UI, or Settings flow.

---

## Resetting Navigation History

Whenever your application begins a new navigation flow, clear the existing navigation history before registering the new root screen.

The recommended approach is to reset the history through your integration bridge:

```csharp
_bridge.ResetHistory();
```

You can also reset it by calling the framework directly:

```csharp
TrailbackFacade.ResetHistory();
```

After resetting the history, show and register the first screen in the new navigation flow.

```csharp
homeScreen.Show();
_bridge.Show(homeScreen);
```

The first screen you register becomes the root of the new navigation flow, and all subsequent Back navigation is resolved relative to that screen.

## When Should I Reset History?

A simple rule works well in most projects:

> Reset the navigation history whenever you're starting a new navigation flow.

If you're unsure, ask yourself:

```text
                    Is this a new navigation flow?
                               │
                  ┌────────────┴────────────┐
                  ↓                         ↓
                 Yes                        No
                  ↓                         ↓
        Reset Navigation History     Keep Existing History
                  ↓                         
        Register the New Root Screen
```

If the answer is **Yes**, reset the history before registering the new UI.

```csharp
_bridge.ResetHistory();
```

```csharp
TrailbackFacade.ResetHistory();
```

Otherwise, leave the existing history in place and continue reporting UI changes as normal.

### Examples

#### ✅ Reset History

* Starting a new game
* Returning to the Main Menu
* Loading another level with a different UI
* Rebuilding the application's UI
* Replacing one UI hierarchy with another

### ❌ Don't Reset History

* Opening or closing popups
* Moving between screens within the same UI
* Loading or unloading additive gameplay scenes while the UI stays active
* Streaming environments
* Loading Addressables without replacing the current UI

---

## Single Scene Workflow

Many Unity projects load a completely new scene for each level.

```text
Level 10
    ↓
Level Complete Screen
    ↓
Player clicks "Next Level"
    ↓
ResetHistory()
    ↓
Load Level 11
    ↓
Register Countdown Screen
    ↓
Continue Navigation
```
> The previous navigation flow has ended, so the history is cleared before the new UI is registered.

A typical flow looks like this:

```text
Reset History
    ↓    
Load Scene
    ↓
Show Root Screen
```

Exactly where this sequence lives depends on your project's architecture. It might be handled by a Level Manager, a Scene Manager, or a `SceneManager.sceneLoaded` callback.

Regardless of the implementation, make sure the navigation history is reset before registering the new root screen.

Reset the history before registering the first screen in the new scene.

---

## Additive Scene Workflow

Projects using additive scenes often keep the UI alive while gameplay scenes are loaded and unloaded independently.

If only the gameplay scene changes, there's usually no reason to reset the navigation history. The current UI is still active, so the existing navigation flow can continue.

```text
Persistent UI Scene
│
├── Home Screen
├── Pause Menu
└── Settings
        │
        │ Navigation History
        ↓
Gameplay Scene
        ↓
Environment A
        ↓
Unload
        ↓
Environment B
```

```text
Gameplay Scene Changed
        ↓
UI Still Exists
        ↓
Keep Existing History
```

---

## Persistent UI Managers

Some projects keep their UI alive with a persistent manager or `DontDestroyOnLoad`.

```text
Application
        │
        ├── UI Manager
        ├── Inventory
        ├── Pause
        └── Settings
```

Since the UI survives scene changes, the navigation history normally should too.

Only reset the history when you're intentionally replacing the current navigation flow.

---

## Destroying Navigation Elements

If a navigation element is being removed permanently, remove it from Trailback before destroying it.

```csharp
_bridge.Hide(settingsScreen);

Destroy(settingsScreen.gameObject);
```

This keeps the navigation history synchronized with the objects that still exist in your scene.

If the object is being destroyed as part of a completely new navigation flow, resetting the history is usually the better option.

```csharp
_bridge.ResetHistory();
```

---

## Best Practices

✔ Reset the history before registering the new root screen.

✔ Reset the history when starting a new navigation flow.

✔ Register the new root screen immediately afterwards.

✔ Keep the existing history when the current navigation flow continues.

✔ Let your navigation flow decide when navigation begins and ends.

---

## Common Mistakes

### Resetting History After Every Scene Change

Not every scene change starts a new navigation flow.

Projects using additive scenes or persistent UI will usually want to keep the existing history.

---

### Forgetting To Reset History

If the previous navigation flow no longer exists, clear the history before registering the new UI.

Otherwise, Back navigation may still reference screens from the previous flow.

### Resetting History Too Late

Reset the history before registering the new root screen.

Resetting it afterwards will also remove the newly registered root from the navigation history.
