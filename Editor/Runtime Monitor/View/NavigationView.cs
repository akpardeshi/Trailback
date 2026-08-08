using ModularForge.Trailback.Core;
using ModularForge.Trailback.Editor.Styling;
using UnityEngine;
using UnityEngine.UIElements;

namespace ModularForge.Trailback.Editor
{
    /// <summary>
    /// Displays the current and previous Trailback navigation elements.
    /// </summary>
    public class NavigationView : VisualElement
    {
        #region Fields

        private EditorUISection _section;
        
        private Label _currentValueLabel;
        private Object _currentObject;
        private Label _currentPingLabel;

        private Object _backTargetObject;
        private Label _backTargetValueLabel;
        private Label _backTargetPingLabel;

        private Label _canGoBackValueLabel;
        
        private Label _blockReasonValueLabel;
        private Label _blockDetailsValueLabel;


        #endregion


        #region Initialization

        /// <summary>
        /// Creates the Navigation view used by the Runtime Monitor.
        /// </summary>
        public NavigationView()
        {
            style.marginTop = 10;
            BuildUI();
        }

        /// <summary>
        /// Builds the Navigation view user interface.
        /// </summary>
        private void BuildUI()
        {
            BuildHeader();
            BuildCurrentSection();
            BuildBackTargetSection();
            BuildCanGoBackSection();
            BuildBlockReasonSection();
            BuildBlockDetailsSection();
        }

        #endregion


        #region Public API

        /// <summary>
        /// Updates the Navigation view using the latest Trailback runtime state.
        /// </summary>
        /// <param name="state">
        /// The current Trailback runtime state.
        /// </param>
        public void UpdateState(TrailbackState state)
        {
            UpdateCurrent(state);
            UpdateBackTarget(state);
            UpdateCanGoBack(state);
            UpdateBlocking(state);
        }

        #endregion


        #region UI Construction

        /// <summary>
        /// Creates the Navigation section header.
        /// </summary>
        private void BuildHeader()
        {
            _section = new EditorUISection("Navigation");
            Add(_section);
        }

        /// <summary>
        /// Creates the Current navigation section.
        /// </summary>
        private void BuildCurrentSection()
        {
            _section.Content.Add(TrailbackEditorUIStyles.CreateFieldLabel("Current"));
            _currentValueLabel = TrailbackEditorUIStyles.CreateValueLabel();
         
            _currentPingLabel =
                TrailbackEditorUIStyles.CreatePingLabel();
            _section.Content.Add(CreateValueRow(_currentValueLabel, _currentPingLabel));
        }

        /// <summary>
        /// Creates the Back Target section.
        /// </summary>
        private void BuildBackTargetSection()
        {
            _section.Content.Add(TrailbackEditorUIStyles.CreateFieldLabel("Back Target"));
            _backTargetValueLabel = TrailbackEditorUIStyles.CreateValueLabel();
            
            _backTargetPingLabel =
                TrailbackEditorUIStyles.CreatePingLabel();
            _section.Content.Add(CreateValueRow(_backTargetValueLabel, _backTargetPingLabel));
        }

        /// <summary>
        /// Creates the Can Go Back section.
        /// </summary>
        private void BuildCanGoBackSection()
        {
            _section.Content.Add(TrailbackEditorUIStyles.CreateFieldLabel("Can Go Back"));
            _canGoBackValueLabel = TrailbackEditorUIStyles.CreateValueLabel();
            _section.Content.Add(_canGoBackValueLabel);
        }

        /// <summary>
        /// Creates the Block Reason section.
        /// </summary>
        private void BuildBlockReasonSection()
        {
            _section.Content.Add(TrailbackEditorUIStyles.CreateFieldLabel("Block Reason"));
            _blockReasonValueLabel = TrailbackEditorUIStyles.CreateValueLabel();
            _section.Content.Add(_blockReasonValueLabel);
        }

        /// <summary>
        /// Creates the Block Details section.
        /// </summary>
        private void BuildBlockDetailsSection()
        {
            _section.Content.Add(TrailbackEditorUIStyles.CreateFieldLabel("Block Details"));
            _blockDetailsValueLabel = TrailbackEditorUIStyles.CreateValueLabel();
            _section.Content.Add(_blockDetailsValueLabel);
        }

        /// <summary>
        /// Creates a row containing a value label and an optional action element.
        /// </summary>
        /// <param name="valueLabel">
        /// The value label.
        /// </param>
        /// <param name="actionElement">
        /// The action element displayed beside the value.
        /// </param>
        private VisualElement CreateValueRow(Label valueLabel, VisualElement actionElement)
        {
            var row = new VisualElement
            {
                style =
                {
                    flexDirection = FlexDirection.Row,
                    justifyContent = Justify.SpaceBetween,
                    alignItems = Align.Center
                }
            };

            row.Add(valueLabel);
            row.Add(actionElement);

            return row;
        }

        #endregion


        #region State Management

        /// <summary>
        /// Updates the Current navigation entry.
        /// </summary>
        private void UpdateCurrent(TrailbackState state)
        {
            _currentObject = state.NavigationSnapshot.Current.UnityObject;
            _currentValueLabel.text = FormatValue(_currentObject != null ? _currentObject.name : null);
            
            _currentPingLabel.userData = _currentObject;
            TrailbackEditorUIStyles.SetPingEnabled(_currentPingLabel, _currentObject != null);
        }

        /// <summary>
        /// Updates the Back Target navigation entry.
        /// </summary>
        private void UpdateBackTarget(TrailbackState state)
        {
            _backTargetObject = state.NavigationSnapshot.BackTarget.UnityObject;
            _backTargetValueLabel.text = FormatValue(_backTargetObject != null ? _backTargetObject.name : null);
            
            _backTargetPingLabel.userData = _backTargetObject;
            TrailbackEditorUIStyles.SetPingEnabled(_backTargetPingLabel, _backTargetObject != null);
        }

        /// <summary>
        /// Updates the Can Go Back state.
        /// </summary>
        private void UpdateCanGoBack(TrailbackState state)
        {
            _canGoBackValueLabel.text = state.NavigationSnapshot.CanGoBack ? "Yes" : "No";
        }

        /// <summary>
        /// Updates the navigation blocking information.
        /// </summary>
        private void UpdateBlocking(TrailbackState state)
        {
            _blockReasonValueLabel.text = state.NavigationSnapshot.BlockReason.ToString();
            _blockDetailsValueLabel.text = FormatValue(state.NavigationSnapshot.BlockDetails);
        }

        /// <summary>
        /// Returns a placeholder when the supplied value is null or empty.
        /// </summary>
        private string FormatValue(string value)
        {
            return string.IsNullOrWhiteSpace(value) ? "-" : value;
        }

        #endregion
    }
}