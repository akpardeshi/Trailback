using ModularForge.Trailback.Core;
using ModularForge.Trailback.Editor.Styling;
using UnityEngine.UIElements;

namespace ModularForge.Trailback.Editor
{
    /// <summary>
    /// Displays Trailback runtime statistics.
    /// </summary>
    public class StatisticsView : VisualElement
    {
        #region Fields
        
        private Label _activeCategoryLabel;
        private Label _historyEntriesLabel;
        private Label _highestPriorityLabel;
        
        private EditorUISection _section;
        
        #endregion
        
        
        #region Initialization
        
        /// <summary>
        /// Creates the Statistics view used by the Runtime Monitor.
        /// </summary>
        public StatisticsView()
        {
            BuildUI();
        }
        
        /// <summary>
        /// Builds the Statistics view user interface.
        /// </summary>
        private void BuildUI()
        {
            BuildSection();
            BuildActiveCategories();
            BuildHistoryEntries();
            BuildHighestPriority();
        }
        
        /// <summary>
        /// Creates the Statistics section header.
        /// </summary>
        private void BuildSection()
        {
            _section = new EditorUISection("Statistics");

            Add(_section);
        }
        
        /// <summary>
        /// Creates the Active Categories statistic.
        /// </summary>
        private void BuildActiveCategories()
        {
            _section.Content.Add(TrailbackEditorUIStyles.CreateFieldLabel("Active Categories"));
            _activeCategoryLabel = TrailbackEditorUIStyles.CreateValueLabel();
            _section.Content.Add(_activeCategoryLabel);
        }
        
        /// <summary>
        /// Creates the History Entries statistic.
        /// </summary>
        private void BuildHistoryEntries()
        {
            _section.Content.Add(TrailbackEditorUIStyles.CreateFieldLabel("History Entries"));
            _historyEntriesLabel = TrailbackEditorUIStyles.CreateValueLabel();
            _section.Content.Add(_historyEntriesLabel);
        }
        
        /// <summary>
        /// Creates the Highest Priority statistic.
        /// </summary>
        private void BuildHighestPriority()
        {
            _section.Content.Add(TrailbackEditorUIStyles.CreateFieldLabel("Highest Priority"));
            _highestPriorityLabel = TrailbackEditorUIStyles.CreateValueLabel();
            _section.Content.Add(_highestPriorityLabel);
        }
        
        #endregion
        
        
        #region Public API
        
        /// <summary>
        /// Updates the Statistics view using the latest Trailback runtime state.
        /// </summary>
        /// <param name="state">
        /// The current Trailback runtime state.
        /// </param>
        public void UpdateState(TrailbackState state)
        {
            _activeCategoryLabel.text = state.ActiveCategoryCount.ToString();
            
            _historyEntriesLabel.text = state.TotalHistoryEntries.ToString();
            
            _highestPriorityLabel.text = state.HighestPriorityCategory;
        }
        
        #endregion
    }
}