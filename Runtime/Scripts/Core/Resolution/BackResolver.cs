using ModularForge.Trailback.Diagnostics;
using UnityEngine;

namespace ModularForge.Trailback.Core
{
    /// <summary>
    /// Resolves navigation information from Trailback history.
    /// </summary>
    /// <remarks>
    /// BackResolver acts as the bridge between history storage and navigation execution.
    ///
    /// This class is responsible for:
    /// - Resolving navigation contexts
    /// - Determining navigation availability
    /// - Building diagnostic state snapshots
    ///
    /// BackResolver does not store history or execute navigation.
    /// </remarks>
    public class BackResolver
    {
        #region Fields
        
        /// <summary>
        /// Navigation history used to resolve navigation relationships.
        /// </summary>
        /// <remarks>
        /// Provides access to the current navigation state stored by Trailback.
        ///
        /// BackResolver uses this history instance to:
        ///
        /// - Resolve the current navigation entry
        /// - Resolve the previous navigation entry
        /// - Evaluate category priorities
        /// - Generate navigation contexts
        ///
        /// The resolver never stores navigation state itself.
        /// All navigation state is sourced from BackHistory.
        /// </remarks>
        private readonly BackHistory _history;
        
        #endregion
        
        
        #region Constructor
        
        /// <summary>
        /// Creates a new navigation resolver.
        /// </summary>
        /// <param name="history">
        /// History storage used for navigation resolution.
        /// </param>
        public BackResolver(BackHistory history)
        {
            _history = history;
        }
        
        #endregion
        
        
        #region Navigation Resolution
        
        /// <summary>
        /// Resolves the current navigation context.
        /// </summary>
        /// <returns>
        /// A navigation context containing the current and previous navigation entries.
        ///
        /// Returns an empty context if no active navigation category can be resolved.
        /// </returns>
        /// <remarks>
        /// Navigation context resolution is based on the highest priority category currently available
        /// in history.
        /// </remarks>
        public BackContext ResolveContext()
        {
            var category = _history.GetHighestPriorityCategory();
            
            if (category == null)
            {
                Debug.LogWarning("[Trailback] Failed to resolve navigation context. No active categories found in history.");

                return new BackContext(null, null);
            }

            return new BackContext(_history.PeekCurrent(), _history.PeekPrevious());
        }

        /// <summary>
        /// Determines whether back navigation can currently be resolved.
        /// </summary>
        /// <returns>
        /// True if navigation history contains a valid back navigation target; otherwise false.
        /// </returns>
        private bool CanResolveBack()
        {
            return _history.CanNavigateBack();
        }

        #endregion
        
        
        #region Diagnostics
        
        
        /// <summary>
        /// Builds a diagnostic snapshot representing Trailback's current runtime state.
        /// </summary>
        /// <returns>
        /// An immutable snapshot containing navigation, history, and blocking information.
        /// </returns>
        /// <remarks>
        /// This method is primarily intended for diagnostics, monitoring, debugging,
        /// and developer tooling.
        /// </remarks>
        public TrailbackState BuildState()
        {
            var context = ResolveContext();
            
            BackBlockReason blockReason = BackBlockReason.None;

            string blockDetails = string.Empty;
            
            if (context.Current != null && context.Current is IBackNavigationBlocker blocker)
            {
                if (blocker.BackNavigationMode == BackNavigationMode.Block)
                {
                    blockReason = BackBlockReason.BlockedByConfiguration;
                    blockDetails = TrailbackDebugUtility.GetDebugName(context.Current);
                }
            }

            bool canGoBack = CanResolveBack();

            if (blockReason == BackBlockReason.BlockedByConfiguration)
            {
                canGoBack = false;
            }
            
            
            return new TrailbackState
            (
                TrailbackDebugUtility.GetDebugName(context.Current) ?? "None",
                TrailbackDebugUtility.GetDebugName(context.Previous) ?? "None",

                canGoBack,

                _history.ActiveCategoryCount,
                 _history.TotalHistoryEntries,
                _history.HighestPriorityCategoryName,

                blockReason,
                blockDetails
            );
        }
        
        #endregion
    }
}