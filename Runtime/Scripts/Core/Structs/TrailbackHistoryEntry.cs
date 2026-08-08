using UnityEngine;

namespace ModularForge.Trailback.Core
{
    /// <summary>
    /// Represents a single immutable navigation history entry captured for diagnostics.
    /// </summary>
    /// <remarks>
    /// Represents an immutable snapshot of a navigation entry at the time it was captured.
    ///
    /// TrailbackHistoryEntry is used throughout Trailback's diagnostic system, including
    /// navigation snapshots, history snapshots, the Runtime Monitor, debugging utilities,
    /// and other developer tools.
    ///
    /// The associated Unity object reference is optional. If a navigation entry isn't
    /// backed by a Unity object, this reference will be null. When available, the
    /// reference allows editor features such as object pinging without making the
    /// diagnostic model dependent on Unity objects.
    /// </remarks>
    public readonly struct TrailbackHistoryEntry
    {
        /// <summary>
        /// Display name of the navigation entry.
        /// </summary>
        public string DisplayName { get; }

        /// <summary>
        /// Navigation category that owns this history entry.
        /// </summary>
        public NavigationCategorySo Category { get; }

        /// <summary>
        /// Unity object associated with this navigation entry, if available.
        /// </summary>
        public Object UnityObject { get; }

        /// <summary>
        /// Creates a diagnostic history entry from a navigable object.
        /// </summary>
        /// <param name="navigable">
        /// Navigation entry captured by this diagnostic snapshot.
        /// </param>
        public TrailbackHistoryEntry(IBackNavigable navigable)
        {
            DisplayName = TrailbackDebugUtility.GetDebugName(navigable);
            Category = navigable.NavigationCategory;

            if (navigable is Object unityObject)
            {
                UnityObject = unityObject;
            }
            else
            {
                UnityObject = null;
            }
        }
    }
}