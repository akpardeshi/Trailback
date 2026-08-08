using System.Collections.Generic;
using ModularForge.Trailback.Core;
using ModularForge.Trailback.Editor.Styling;
using UnityEngine;
using UnityEngine.UIElements;

namespace ModularForge.Trailback.Editor
{
    /// <summary>
    /// Displays Trailback's navigation history grouped by navigation category.
    /// </summary>
    public class HistoryView : VisualElement
    {
        #region Fields

        private VisualElement _entryContainer;
        
        private ScrollView _scrollView;
        
        private VisualElement _content;

        #endregion


        #region Initialization

        /// <summary>
        /// Creates the History view used by the Runtime Monitor.
        /// </summary>
        public HistoryView()
        {
            BuildUI();
        }

        /// <summary>
        /// Builds the History view user interface.
        /// </summary>
        private void BuildUI()
        {
            BuildHeader();
            BuildScrollView();
            BuildEntryContainer();
        }

        #endregion


        #region Public API

        /// <summary>
        /// Updates the history view using the latest Trailback runtime state.
        /// </summary>
        public void UpdateState(TrailbackState state)
        {
            ClearHistory();
            PopulateHistory(state);
        }

        #endregion


        #region UI Construction

        /// <summary>
        /// Creates a visual row representing a single history entry.
        /// </summary>
        /// <param name="entry">
        /// The history entry to display.
        /// </param>
        private VisualElement CreateEntryRow(TrailbackHistoryEntry entry)
        {
            var row = new VisualElement
            {
                style =
                {
                    marginTop = 1,
                    marginBottom = 1,
                    marginLeft = 8,
                    flexDirection = FlexDirection.Row,
                    justifyContent = Justify.SpaceBetween,
                    alignItems = Align.Center
                }
            };

            row.Add(new Label(entry.DisplayName));

            var pingLabel = TrailbackEditorUIStyles.CreatePingLabel();
            pingLabel.userData = entry.UnityObject;
            row.Add(pingLabel);

            return row;
        }

        /// <summary>
        /// Creates the History section header.
        /// </summary>
        private void BuildHeader()
        {
            var section = new EditorUISection("History");
            Add(section);
            _content = section.Content;
        }

        /// <summary>
        /// Creates a foldout used to display a navigation category.
        /// </summary>
        /// <param name="category">
        /// The navigation category.
        /// </param>
        /// <param name="entryCount">
        /// The number of history entries in the category.
        /// </param>
        private Foldout CreateCategoryFoldout(NavigationCategorySo category, int entryCount)
        {
            return new Foldout
            {
                text = $"{category.CategoryName} ({entryCount})",
                value = true
            };
        }
        
        /// <summary>
        /// Creates the scroll view used to display navigation history.
        /// </summary>
        private void BuildScrollView()
        {
            _scrollView = new ScrollView();
            _scrollView.style.flexGrow = 1;
            _content.Add(_scrollView);
        }
        
        /// <summary>
        /// Creates the container that hosts all history category foldouts.
        /// </summary>
        private void BuildEntryContainer()
        {
            _entryContainer = new VisualElement
            {
                style =
                {
                    flexGrow = 1,
                    paddingRight = 12
                }
            };

            _scrollView.contentContainer.style.paddingTop = 5;
            _scrollView.contentContainer.style.marginTop = 5;
            
            _scrollView.contentContainer.Add(_entryContainer);
        }
        
        #endregion


        #region State Management

        /// <summary>
        /// Removes all rendered history entries.
        /// </summary>
        private void ClearHistory()
        {
            _entryContainer.Clear();
        }

        /// <summary>
        /// Populates the History view using the supplied Trailback state.
        /// </summary>
        /// <param name="state">
        /// The current Trailback runtime state.
        /// </param>
        private void PopulateHistory(TrailbackState state)
        {
            NavigationCategorySo currentCategory = null;
            Foldout currentFoldout = null;
            int index = 0;
            
            foreach (var entry in state.HistorySnapshot.Entries)
            {
                if (currentCategory != entry.Category)
                {
                    currentCategory = entry.Category;

                    int entryCount = GetCategoryEntryCount(state.HistorySnapshot.Entries, index);
                    currentFoldout = CreateCategoryFoldout(currentCategory, entryCount);
                    currentFoldout.style.marginBottom = 5;
                    
                    var titleLabel = currentFoldout.Q<Label>();
                    titleLabel.style.unityFontStyleAndWeight = FontStyle.Bold;
                    
                    _entryContainer.Add(currentFoldout);
                }

                currentFoldout?.Add(CreateEntryRow(entry));

                index++;
            }
        }

        /// <summary>
        /// Returns the number of consecutive history entries belonging to the specified category.
        /// </summary>
        /// <param name="entries">
        /// The ordered history entries.
        /// </param>
        /// <param name="startIndex">
        /// The index of the first entry in the category.
        /// </param>
        private int GetCategoryEntryCount(IReadOnlyList<TrailbackHistoryEntry> entries, int startIndex)
        {
            var category = entries[startIndex].Category;
            int count = 0;

            for (int i = startIndex; i < entries.Count; i++)
            {
                if (entries[i].Category != category)
                {
                    break;
                }

                count++;
            }

            return count;
        }
        
        #endregion
    }
}