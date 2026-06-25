using System;
using UnityEngine;

namespace ModularForge.Trailback.Core
{
    
    /// <summary>
    /// Central entry point for Trailback's navigation history system.
    /// </summary>
    /// <remarks>
    /// Trailback coordinates history storage, navigation resolution, navigation execution,
    /// diagnostics, and state notifications.
    ///
    /// Most applications interact exclusively with this class rather than using framework
    /// components directly.
    ///
    /// Trailback is responsible for:
    /// - Navigation history registration
    /// - Back navigation requests
    /// - Navigation state reporting
    /// - Runtime diagnostics
    /// - Navigation lifecycle events
    ///
    /// Trailback does not directly control UI or execute navigation behavior.
    /// </remarks>
    public static class Trailback
    {
        #region Events

        /// <summary>
        /// Invoked when a back navigation request reaches the root of the navigation history.
        /// </summary>
        public static event Action OnNavigationRootReached;

        /// <summary>
        /// Invoked whenever Trailback's runtime state changes.
        /// </summary>
        public static event Action<TrailbackState> OnStateChanged;

        #endregion

        #region Dependencies

        /// <summary>
        /// Stores Trailback's navigation history.
        /// </summary>
        /// <remarks>
        /// Responsible for tracking navigation entries, category history, and navigation order.
        ///
        /// All navigation state is persisted through this history instance.
        /// </remarks>
        private static readonly BackHistory _history = new();

        /// <summary>
        /// Resolves navigation targets from the current history state.
        /// </summary>
        /// <remarks>
        /// Uses the navigation history to determine:
        ///
        /// - Current navigation entry
        /// - Previous navigation entry
        /// - Category priority resolution
        /// - Navigation context generation
        ///
        /// Trailback delegates all navigation resolution responsibilities to this component.
        /// </remarks>
        private static BackResolver _resolver = new(_history);

        /// <summary>
        /// Executes navigation transitions resolved by Trailback.
        /// </summary>
        /// <remarks>
        /// Trailback determines what navigation should occur. occur.
        ///
        /// The navigation handler determines how that navigation is performed.
        ///
        /// Typical responsibilities include:
        ///
        /// - Showing previous screens
        /// - Hiding current screens
        /// - Playing transitions
        /// - Triggering animations
        ///
        /// Navigation execution remains application specific and is therefore delegated to
        /// an external handler.
        /// </remarks>
        private static IBackNavigationHandler _navigationHandler;

        #endregion


        #region Navigation

        /// <summary>
        /// Attempts to perform a back navigation operation.
        /// </summary>
        /// <returns>
        /// True if navigation was executed successfully, otherwise false.
        /// </returns>
        /// <remarks>
        /// Navigation may fail when:
        /// - Navigation is blocked
        /// - History cannot be resolved
        /// - The navigation root has been reached
        /// </remarks>
        public static bool Back()
        {
            var context = _resolver.ResolveContext();

            if (context.Current is IBackNavigationBlocker blocker)
            {
                if (blocker.BackNavigationMode == BackNavigationMode.Block)
                {
                    //Debug.Log($"[Trailback] Back navigation blocked by {context.Current}");
                    return false;
                }
            }

            if (context.Previous == null && context.Current == null)
            {
                Debug.LogWarning("[Trailback] Failed to resolve back navigation context.");

                return false;
            }

            var item = _history.Consume();

            if (item == null)
            {
                Debug.Log("[Trailback] Navigation root reached.");

                NotifyNavigationRootReached();
                return false;
            }

            _navigationHandler?.NavigateBackTo(context);
            NotifyStateChanged();

            return true;
        }

        #endregion


        #region History Reporting

        /// <summary>
        /// Registers a navigation entry as visible and adds it to navigation history.
        /// </summary>
        /// <param name="element">
        /// Navigation entry that became visible.
        /// </param>
        public static void ReportShown(IBackNavigable element)
        {
            if (element == null)
            {
                Debug.LogWarning("[Trailback] Cannot report null element as shown.");

                return;
            }

            _history.Push(element);
            NotifyStateChanged();
        }

        /// <summary>
        /// Registers a navigation entry as hidden and removes it from navigation history.
        /// </summary>
        /// <param name="element">
        /// Navigation entry that became hidden.
        /// </param>
        public static void ReportHidden(IBackNavigable element)
        {
            if (element == null)
            {
                Debug.LogWarning("[Trailback] Cannot report null element as hidden.");

                return;
            }

            _history.Remove(element);
            NotifyStateChanged();
        }

        /// <summary>
        /// Removes all navigation history from Trailback.
        /// </summary>
        /// <remarks>
        /// This operation permanently clears all registered navigation entries.
        /// </remarks>
        public static void ResetHistory()
        {
            _history.ClearAll();
        }

        #endregion


        #region State

        /// <summary>
        /// Returns a snapshot of Trailback's current runtime state.
        /// </summary>
        /// <returns>
        /// Immutable runtime state snapshot.
        /// </returns>
        public static TrailbackState GetState()
        {
            return _resolver.BuildState();
        }

        /// <summary>
        /// Broadcasts the current Trailback state to all state change subscribers.
        /// </summary>
        private static void NotifyStateChanged()
        {
            OnStateChanged?.Invoke(GetState());
        }

        #endregion


        #region Configuration

        /// <summary>
        /// Assigns the navigation handler used to execute resolved navigation operations.
        /// </summary>
        /// <param name="handler">
        /// Navigation handler implementation.
        /// </param>
        public static void SetNavigationHandler(IBackNavigationHandler handler)
        {
            _navigationHandler = handler;
        }

        #endregion


        #region Internal Events

        /// <summary>
        /// Broadcasts the navigation root reached event.
        /// </summary>
        private static void NotifyNavigationRootReached()
        {
            OnNavigationRootReached?.Invoke();
        }

        #endregion
    }
}