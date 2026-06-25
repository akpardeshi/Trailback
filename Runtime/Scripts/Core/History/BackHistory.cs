using System.Collections.Generic;
using ModularForge.Trailback.Diagnostics;
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
    public sealed class BackHistory
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
        
        
        #region History  Registration

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
        /// Returns the navigation entry that would become active after a successful back navigation.
        /// </summary>
        /// <returns>
        /// The previous navigation entry from the highest priority category, or null if no entry exists.
        /// </returns>
        public IBackNavigable PeekPrevious()
        {
            var category = GetHighestPriorityCategory();

            if (category == null)
            {
                return null;
            }

            return PeekPrevious(category);
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
                Debug.LogError("[Trailback] Cannot push null item.");

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
        /// Returns the previous navigation entry from the specified category.
        /// </summary>
        /// <param name="category">
        /// Category whose previous entry should be retrieved.
        /// </param>
        /// <returns>
        /// Previous navigation entry, or null if one cannot be resolved.
        /// </returns>
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

        #endregion
    }
}