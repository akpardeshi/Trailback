# Trailback 1.0.0-alpha Release Notes

## Overview

Trailback 1.0.0-alpha is the first public alpha release of the framework.

This release focuses on validating the core navigation workflow, integration experience, tooling, and documentation before a production-ready release.

Trailback provides a structured solution for managing back navigation across screens, popups, dialogs, and other navigable UI elements in Unity.

---

## Highlights

### Navigation History

Automatically track navigation state and return to previously visited UI elements.

### Category Priorities

Resolve higher-priority UI layers, such as popups and dialogs, before lower-priority screens.

### Navigation Blockers

Prevent navigation during critical workflows such as confirmation dialogs and unsaved changes.

### Root Protection

Protect category roots from being consumed and react when navigation reaches the root boundary through the `OnNavigationRootReached` event.

### Input Abstraction

Support both:

* Legacy Input Manager
* Unity Input System

through a common navigation workflow.

### Bridge Integration

Integrate Trailback through an application-facing bridge layer to keep project code isolated from framework internals.

### Runtime Monitor

Inspect navigation history, active categories, blockers, and runtime state in real time.

### Quick Validation

Validate common setup issues including:

* Missing Components
* Duplicate Components
* Disabled Components

### Editor Tooling

Create common Trailback components directly from Unity editor menus.

### Flexible Event Integration

Support multiple event workflows:

* Inspector Events
* Code Events
* Bridge Events
* Direct Framework Events

---

## Included Documentation

This release includes:

* README
* Installation Guide
* Quick Start Guide
* Validation Documentation
* Runtime Monitor Documentation
* Event Subscription Documentation
* Navigation Blocker Documentation
* Root Reached Event Documentation

---

## Demo Content

A demo scene is included to showcase:

* Navigation History
* Category Priorities
* Navigation Blockers
* Runtime Monitor
* Legacy Input Integration
* Input System Integration
* Root Reached Events

---

## Supported Unity Versions

* Unity 2022.3 LTS and newer

---

## Alpha Status

This release is intended for evaluation, testing, and feedback.

Areas where feedback is especially valuable:

* Integration Experience
* Documentation Clarity
* Runtime Monitor Usability
* Validation Workflow
* Navigation Category Design
* Navigation Blocker Workflow
* Root Protection Workflow

API and tooling may evolve based on feedback received during alpha testing.

Thank you for helping test Trailback.
