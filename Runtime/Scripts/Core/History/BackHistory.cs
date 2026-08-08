using System.Collections.Generic;
using UnityEngine;

namespace ModularForge.Trailback.Core
{
    /// <summary>
    /// Stores and manages navigation history for Trailback.
    /// </summary>
    /// <remarks>
    /// BackHistory maintains navigation entries grouped by navigation category and provides the core
    /// history operations used by Trailback.
    ///
    /// This class is responsible for:
    /// - Navigation entry registration
    /// - Navigation entry removal
    /// - History consumption
    /// - Category-based history organization
    /// - Navigation state queries
    ///
    /// BackHistory does not perform navigation resolution or navigation execution.
    /// </remarks>
    public class BackHistory
    {

        #region Fields
        
        private readonly Dictionary<NavigationCategorySo, Stack<IBackNavigable>> _historyByCategory;
        
        #endregion
        
        
        #region Constructor
        
        /// <summary>
        /// Creates a new navigation history store.
        /// </summary>
        public BackHistory()
        {
            _historyByCategory = new Dictionary<NavigationCategorySo, Stack<IBackNavigable>>();
        }
        
        #endregion
        
        
        #region History Registration

        /// <summary>
        /// Registers a navigation entry in history.
        /// </summary>
        /// <param name="item">
        /// Navigation entry to register.
        /// </param>
        /// <remarks>
        /// The entry is added to the history stack associated with its navigation category.
        ///
        /// Duplicate handling is determined by the category's configured duplicate policy.
        /// </remarks>
        public void Push(IBackNavigable item)
        {
            if (!Validate(item))
            {
                return;
            }

            var category = item.NavigationCategory;
            var stack = GetOrCreateCategoryStack(category);
            
            bool contains = stack.Contains(item);

            if (contains)
            {
                Debug.LogWarning(
                    $"[Trailback] Duplicate target detected." +
                    $"\nTarget: {TrailbackDebugUtility.GetDebugName(item)}" +
                    $"\nCategory: {category.name}" +
                    $"\nPolicy: {category.DuplicatePolicy}");

                if (category.DuplicatePolicy == DuplicatePolicy.Ignore)
                {
                    return;
                }
            }

            stack.Push(item);
        }
        
        /// <summary>
        /// Removes a navigation entry from history.
        /// </summary>
        /// <param name="item">
        /// Navigation entry to remove.
        /// </param>
        /// <returns>
        /// True if the entry was removed successfully; otherwise false.
        /// </returns>
        public bool Remove(IBackNavigable item)
        {
            if (!Validate(item))
            {
                return false;
            }

            var category = item.NavigationCategory;

            if (!_historyByCategory.TryGetValue(category, out var stack))
            {
                return false;
            }

            return RemoveFromStack(stack, item);
        }
        
        #endregion
        
        
        #region History Navigation
        
        /// <summary>
        /// Returns the currently active navigation entry.
        /// </summary>
        /// <returns>
        /// The active navigation entry from the highest priority category, or null if no entry exists.
        /// </returns>
        public IBackNavigable PeekCurrent()
        {
            var category = GetHighestPriorityCategory();

            if (category == null)
            {
                return null;
            }

            return Peek(category);
        }

        /// <summary>
        /// Removes and returns the current navigation entry from the highest priority category.
        /// </summary>
        /// <returns>
        /// The consumed navigation entry, or null if navigation cannot continue.
        /// </returns>
        /// <remarks>
        /// Root protection rules are respected during consumption.
        /// </remarks>
        public IBackNavigable Consume()
        {
            var category = GetHighestPriorityCategory();

            if (category == null)
            {
                return null;
            }

            var stack = GetOrCreateCategoryStack(category);

            if (category.ProtectRootElement && stack.Count <= 1)
            {
                return null;
            }

            return stack.Pop();
        }
        
        /// <summary>
        /// Determines whether any category currently contains navigable history.
        /// </summary>
        /// <returns>
        /// True if back navigation can be performed; otherwise false.
        /// </returns>
        public bool CanNavigateBack()
        {
            foreach (var pair in _historyByCategory)
            {
                var category = pair.Key;
                var stack = pair.Value;

                if (category.ProtectRootElement)
                {
                    if (stack.Count > 1)
                    {
                        return true;
                    }
                }
                else
                {
                    if (stack.Count > 0)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
        
        /// <summary>
        /// Returns the navigation entry that would become active after a successful
        /// back navigation.
        /// </summary>
        /// <returns>
        /// The resolved back navigation target, or <see langword="null"/> if no valid
        /// target exists.
        /// </returns>
        /// <remarks>
        /// If the active category contains more than one navigation entry, the previous
        /// entry in that category is returned.
        ///
        /// Otherwise, Trailback searches the remaining categories in priority order and
        /// returns the most recent entry from the highest-priority category that still
        /// contains navigation history.
        /// </remarks>
        internal IBackNavigable ResolveBackTarget()
        {
            var activeCategory = GetHighestPriorityCategory();

            if (activeCategory == null)
            {
                return null;
            }

            if (!_historyByCategory.TryGetValue(activeCategory, out var stack))
            {
                return null;
            }

            // Back stays within the current category.
            if (stack.Count > 1)
            {
                return PeekPrevious(activeCategory);
            }

            // Back leaves the current category.
            var nextCategory = GetHighestPriorityCategoryExcluding(activeCategory);

            if (nextCategory == null)
            {
                return null;
            }

            return Peek(nextCategory);
        }
        
        #endregion
        
        
        #region History State
        
        /// <summary>
        /// Gets the number of categories currently tracked by the history system.
        /// </summary>
        public int ActiveCategoryCount => _historyByCategory.Count;
        
        /// <summary>
        /// Gets the total number of navigation entries currently stored across all categories.
        /// </summary>
        public int TotalHistoryEntries
        {
            get
            {
                int count = 0;

                foreach (var pair in _historyByCategory)
                {
                    count += pair.Value.Count;
                }

                return count;
            }
        }

        /// <summary>
        /// Gets the name of the highest priority category that currently contains navigation history.
        /// </summary>
        public string HighestPriorityCategoryName
        {
            get
            {
                var category = GetHighestPriorityCategory();

                return category == null ? "None" : category.CategoryName;
            }
        }
        
        /// <summary>
        /// Returns the active navigation category with the highest priority.
        /// </summary>
        /// <returns>
        /// The highest priority category that currently contains navigation history, or null if none
        /// exist.
        /// </returns>
        public NavigationCategorySo GetHighestPriorityCategory()
        {
            NavigationCategorySo winner = null;

            foreach (var pair in _historyByCategory)
            {
                if (pair.Value.Count == 0)
                {
                    continue;
                }

                if (winner == null || pair.Key.Priority > winner.Priority)
                {
                    winner = pair.Key;
                }
            }

            return winner;
        }
        
        #endregion

        
        #region Diagnostics
        
        /// <summary>
        /// Builds an immutable snapshot of the current navigation history.
        /// </summary>
        /// <returns>
        /// A history snapshot representing the current state of Trailback's
        /// navigation history.
        /// </returns>
        /// <remarks>
        /// This method is intended for diagnostics, runtime monitoring,
        /// debugging, and developer tooling.
        /// </remarks>
        internal TrailbackHistorySnapshot BuildHistorySnapshot()
        {
            var entries = CreateHistoryEntries();

            return new TrailbackHistorySnapshot(entries);
        }
        
        /// <summary>
        /// Returns the current navigation entry as a diagnostic history entry.
        /// </summary>
        /// <returns>
        /// The current navigation entry represented as a
        /// <see cref="TrailbackHistoryEntry"/>.
        /// </returns>
        public TrailbackHistoryEntry GetCurrentHistoryEntry()
        {
            return CreateHistoryEntry(PeekCurrent());
        }
        
        /// <summary>
        /// Returns the resolved back navigation target as a diagnostic history entry.
        /// </summary>
        /// <returns>
        /// The resolved back navigation target represented as a
        /// <see cref="TrailbackHistoryEntry"/>.
        /// </returns>
        internal TrailbackHistoryEntry GetBackTargetHistoryEntry()
        {
            return CreateHistoryEntry(ResolveBackTarget());
        }
        
        #endregion
        

        #region History Maintenance

        /// <summary>
        /// Removes all navigation history from every category.
        /// </summary>
        /// <remarks>
        /// This operation permanently clears all registered navigation entries and resets the history state.
        /// </remarks>
        public void ClearAll()
        {
            _historyByCategory.Clear();
        }

        #endregion
        
        
        #region Validation
        
        /// <summary>
        /// Validates a navigation entry before it is processed by the history system.
        /// </summary>
        /// <param name="item">
        /// Navigation entry to validate.
        /// </param>
        /// <returns>
        /// True if the navigation entry is valid, otherwise false.
        /// </returns>
        private bool Validate(IBackNavigable item)
        {
            if (item == null)
            {
                Debug.LogError("[Trailback] Navigation entry cannot be null.");

                return false;
            }

            if (item.NavigationCategory == null)
            {
                Debug.LogError($"[Trailback] Missing NavigationCategory on {item}.");

                return false;
            }

            return true;
        }
        
        #endregion
        
        
        #region Internal Helpers

        /// <summary>
        /// Removes a navigation entry from the specified history stack while preserving stack order.
        /// </summary>
        /// <param name="stack">
        /// Stack that contains the navigation entry.
        /// </param>
        /// <param name="target">
        /// Navigation entry to remove.
        /// </param>
        /// <returns>
        /// True if the navigation entry was removed, otherwise false.
        /// </returns>
        private bool RemoveFromStack(Stack<IBackNavigable> stack, IBackNavigable target)
        {
            if (target == null)
            {
                return false;
            }

            if (!stack.Contains(target))
            {
                return false;
            }

            Stack<IBackNavigable> temp = new();

            bool removed = false;

            while (stack.Count > 0)
            {
                var current = stack.Pop();

                if (!removed && current == target)
                {
                    removed = true;
                    continue;
                }

                temp.Push(current);
            }

            while (temp.Count > 0)
            {
                stack.Push(temp.Pop());
            }

            return removed;
        }
        
        /// <summary>
        /// Returns the history stack associated with the specified category, creating it if necessary.
        /// </summary>
        /// <param name="category">
        /// Navigation category whose history stack should be retrieved.
        /// </param>
        /// <returns>
        /// Existing or newly created history stack.
        /// </returns>
        private Stack<IBackNavigable> GetOrCreateCategoryStack(NavigationCategorySo category)
        {
            if (_historyByCategory.TryGetValue(category, out var stack)) return stack;
            
            stack = new Stack<IBackNavigable>();
            
            _historyByCategory.Add(category, stack);

            return stack;
        }
        
        /// <summary>
        /// Returns the active navigation entry from the specified category.
        /// </summary>
        /// <param name="category">
        /// Category whose active entry should be retrieved.
        /// </param>
        /// <returns>
        /// Active navigation entry, or null if no entry exists.
        /// </returns>
        private IBackNavigable Peek(NavigationCategorySo category)
        {
            if (category == null)
            {
                Debug.LogError("[Trailback] Category is null.");

                return null;
            }

            if (!_historyByCategory.TryGetValue(category, out var stack))
            {
                return null;
            }

            if (stack.Count == 0)
            {
                return null;
            }

            return stack.Peek();
        }

        /// <summary>
        /// Returns the previous navigation entry within the specified category.
        /// </summary>
        /// <param name="category">
        /// Category whose previous navigation entry should be retrieved.
        /// </param>
        /// <returns>
        /// The previous navigation entry within the specified category, or null if
        /// fewer than two entries exist.
        /// </returns>
        /// <remarks>
        /// This method operates only on the specified category's history stack.
        /// It does not resolve Trailback's overall back navigation target.
        /// </remarks>
        private IBackNavigable PeekPrevious(NavigationCategorySo category)
        {
            if (!_historyByCategory.TryGetValue(category, out var stack))
            {
                return null;
            }

            if (stack.Count < 2)
            {
                return null;
            }

            var items = stack.ToArray();

            return items[1];
        }
        
        /// <summary>
        /// Creates an ordered collection of diagnostic history entries.
        /// </summary>
        /// <returns>
        /// A collection of history entries ordered by category priority and
        /// navigation order.
        /// </returns>
        private List<TrailbackHistoryEntry> CreateHistoryEntries()
        {
            var entries = new List<TrailbackHistoryEntry>();

            var categories = new List<NavigationCategorySo>(_historyByCategory.Keys);

            categories.Sort((left, right) => right.Priority.CompareTo(left.Priority));

            foreach (var category in categories)
            {
                var stack = _historyByCategory[category];

                foreach (var navigable in stack)
                {
                    entries.Add(CreateHistoryEntry(navigable));
                }
            }

            return entries;
        }
        
        /// <summary>
        /// Creates a diagnostic history entry for the specified navigation entry.
        /// </summary>
        /// <param name="navigable">
        /// Navigation entry to convert.
        /// </param>
        /// <returns>
        /// A diagnostic representation of the navigation entry.
        /// Returns the default value if the navigation entry is null.
        /// </returns>
        private static TrailbackHistoryEntry CreateHistoryEntry(IBackNavigable navigable)
        {
            return navigable == null ? default : new TrailbackHistoryEntry(navigable);
        }

        /// <summary>
        /// Returns the highest priority navigation category, excluding the specified
        /// category.
        /// </summary>
        /// <param name="excludedCategory">
        /// Category to exclude from the search.
        /// </param>
        /// <returns>
        /// The highest priority remaining category that contains navigation history,
        /// or null if no matching category exists.
        /// </returns>
        private NavigationCategorySo GetHighestPriorityCategoryExcluding(NavigationCategorySo excludedCategory)
        {
            NavigationCategorySo winner = null;

            foreach (var pair in _historyByCategory)
            {
                if (pair.Key == excludedCategory)
                {
                    continue;
                }

                if (pair.Value.Count == 0)
                {
                    continue;
                }

                if (winner == null || pair.Key.Priority > winner.Priority)
                {
                    winner = pair.Key;
                }
            }

            return winner;
        }

        #endregion
    }
}