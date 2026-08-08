# Trailback 1.1.0-alpha Release Notes

** Release Date** 6 August 2026

## Added

### Trailback Runtime Debugger

`Trailback Runtime Debugger` allows the develoepr to debug `Trailback` navigation history. 

### Improved

* Simplified the README.md Quick Start Guide.
* Added `TrailbackDeveloperGuide.md` with detailed feature integration details.

---

# Trailback 1.0.0-alpha Release Notes

## Overview

Welcome to the first public alpha release of Trailback.

This release introduces the core framework along with the documentation, demo project, and reference samples needed to start integrating Trailback into your own projects.

Trailback is built to simplify back navigation in Unity applications. It keeps track of navigation history, resolves which UI element should respond to Back, and leaves your application in control of how navigation is performed.

---

## Highlights

### Navigation History

Keep track of screens, and popups automatically, making it easy to return to previously visited UI.

### Navigation Categories

Group navigation into independent categories and control which one responds first using configurable priorities.

### Navigation Blocking

Temporarily prevent Back navigation while users complete workflows such as confirmation dialogs, loading operations, or unsaved changes.

### Root Protection

Keep the root of your navigation flow in place and respond when users can't navigate back any further through the `OnNavigationRootReached` event.

### Flexible Integration

Integrate Trailback into your existing project through `TrailbackIntegrationBridge`.

Reference implementations are included for:

* Legacy Input Manager
* Unity Input System
* Custom `BackInputSource` implementations

### Runtime Monitor

Monitor navigation history, active categories, blockers, and other runtime information while your application is running.

### Reference Samples

This release includes reference samples for:

* Legacy Input Manager
* Unity Input System
* Runtime Monitor
* Bridge-Based Integration

---

## Documentation

Documentation included with this release:

* README
* Features Guide
* Reference Samples Guide
* Handling Scene Changes Guide
* Troubleshooting Guide

---

## Complete UGUI Demo

A complete UGUI demo project is included to demonstrate Trailback in a working application.

It covers:

* Navigation History
* Navigation Categories
* Navigation Blocking
* Root Protection
* Navigation Root Reached
* Bridge-Based Integration
* Legacy Input Manager
* Unity Input System
* Runtime Monitor

---

## Supported Unity Versions

* Unity 2022.3 LTS and Above

---

## Alpha Status

Trailback is ready for evaluation and community testing.

During the alpha period, I'm especially interested in feedback on:

* Integration experience
* Documentation
* API design
* Runtime Monitor
* Navigation workflows
* Reference samples
* Compatibility across Unity versions

Some APIs, samples, and documentation may continue to evolve as feedback comes in.

If you run into an issue or have suggestions for improving Trailback, please open an issue on GitHub.

Thanks for trying Trailback and helping shape the first stable release.