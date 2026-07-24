using UnityEngine;

namespace ModularForge.Trailback.Core
{
    /// <summary>
    /// Defines how a group of navigation entries behaves within Trailback's history system.
    /// </summary>
    /// <remarks>
    /// Navigation categories are shared configuration assets that control history behavior, duplicate
    /// handling, priority resolution, and root element protection.
    ///
    /// Multiple navigation entries can reference the same category when they require identical
    /// navigation behavior.
    /// </remarks>
    [CreateAssetMenu(fileName = "NavigationCategory", menuName = "Trailback/Navigation/Navigation Category", order = 0)]
    public class NavigationCategorySo : ScriptableObject
    {
        [Header("Category Information")] [SerializeField]
        [Tooltip("User-friendly name displayed by diagnostics, runtime monitoring, and debugging tools.")]
        private string categoryName;

        /// <summary>
        /// User-friendly name displayed by diagnostics, monitoring tools, and debugging utilities.
        /// </summary>
        public string CategoryName => categoryName;

        [Header("Navigation Resolution")] [SerializeField]
        [Tooltip("Determines which category receives navigation priority when multiple categories contain navigation history. Higher values take precedence.")]
        private int priority;

        /// <summary>
        /// Determines the priority used when Trailback resolves navigation across multiple categories.
        /// Higher values take precedence.
        /// </summary>
        public int Priority => priority;

        [Header("History Behavior")] [SerializeField]
        [Tooltip("Controls how Trailback handles attempts to register a navigation entry that already exists in this category.")]
        private DuplicatePolicy duplicatePolicy;

        /// <summary>
        /// Determines how Trailback handles attempts to register duplicate navigation entries
        /// within this category.
        /// </summary>
        public DuplicatePolicy DuplicatePolicy => duplicatePolicy;

        [Tooltip("Prevents Trailback from removing the final navigation entry in this category. Useful for categories that should always maintain a root screen.")]
        [SerializeField] private bool protectRootElement;

        /// <summary>
        /// Prevents Trailback from consuming the final navigation entry in this category.
        /// </summary>
        public bool ProtectRootElement => protectRootElement;
    }
}