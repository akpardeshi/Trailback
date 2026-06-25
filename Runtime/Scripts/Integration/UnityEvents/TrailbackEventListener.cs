using UnityEngine;
using UnityEngine.Events;

namespace ModularForge.Trailback.Integration
{
    /// <summary>
    /// Bridges Trailback navigation events to UnityEvents.
    /// </summary>
    /// <remarks>
    /// This component allows designers to respond to Trailback events directly from the Unity Inspector
    /// without writing code.
    ///
    /// TrailbackEventListener listens for framework events and forwards them to configurable UnityEvents.
    ///
    /// This component is intended for integration and designer-facing workflows.
    /// </remarks>
    public sealed class TrailbackEventListener : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("Invoked when Trailback reaches the root of the navigation history and no further back navigation can be performed.")]
        private UnityEvent onNavigationRootReached;

        /// <summary>
        /// Subscribes to Trailback events when the component becomes active.
        /// </summary>
        private void OnEnable()
        {
            ModularForge.Trailback.Core.Trailback.OnNavigationRootReached += HandleNavigationRootReached;
        }

        /// <summary>
        /// Unsubscribes from Trailback events when the component becomes inactive.
        /// </summary>
        private void OnDisable()
        {
            ModularForge.Trailback.Core.Trailback.OnNavigationRootReached -= HandleNavigationRootReached;
        }

        /// <summary>
        /// Invokes the configured UnityEvent when Trailback reaches the navigation root.
        /// </summary>
        private void HandleNavigationRootReached()
        {
            onNavigationRootReached?.Invoke();
        }
    }
}