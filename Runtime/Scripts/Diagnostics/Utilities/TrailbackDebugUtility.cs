using ModularForge.Trailback.Core;
using UnityEngine;

namespace ModularForge.Trailback.Diagnostics
{
    /// <summary>
    /// Provides helper methods used by Trailback's diagnostic and debugging systems.
    /// </summary>
    /// <remarks>
    /// TrailbackDebugUtility centralizes common formatting and display logic used by
    /// monitoring, debugging, and diagnostic tools.
    ///
    /// This utility does not participate in navigation, history management, or
    /// navigation execution.
    /// </remarks>
    public static class TrailbackDebugUtility
    {
        /// <summary>
        /// Returns a developer-friendly name for the specified navigation entry.
        /// </summary>
        /// <param name="item">
        /// Navigation entry whose display name should be resolved.
        /// </param>
        /// <returns>
        /// The Unity object name when the navigation entry derives from UnityEngine.Object,
        /// otherwise the navigation entry's type name.
        /// </returns>
        public static string GetDebugName(IBackNavigable item)
        {
            switch (item)
            {
                case null:
                    return "None";
                
                case Object unityObject:
                    return unityObject.name;
                
                default:
                    return item.GetType().Name;
            }
        }
    }
}