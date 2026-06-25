using System.Text;
using TMPro;
using ModularForge.Trailback.Core;
using UnityEngine;
using UnityEngine.Serialization;

namespace ModularForge.Trailback.Diagnostics
{
    /// <summary>
    /// Displays Trailback runtime diagnostics and history information.
    /// </summary>
    /// <remarks>
    /// TrailbackMonitorView provides a simple runtime monitor for inspecting Trailback state during
    /// development and debugging.
    ///
    /// The monitor automatically listens for state changes and updates its UI when navigation
    /// history changes.
    ///
    /// This component is intended for diagnostics, debugging, and demonstration purposes.
    /// </remarks>
    public class TrailbackMonitorView : MonoBehaviour
    {
        #region UI References

        [Header("UI")]
        [SerializeField]
        [Tooltip("Text element used to display the current Trailback navigation state.")]
        private TextMeshProUGUI trailbackStateText;

        [SerializeField]
        [Tooltip("Text element used to display Trailback history statistics and category information.")]
        private TextMeshProUGUI historyStatsText;

        #endregion
        

        #region Runtime Data

        /// <summary>
        /// Reusable builder used to generate navigation state text without additional allocations.
        /// </summary>
        private readonly StringBuilder _stateBuilder = new();

        /// Reusable builder used to generate navigation statistics text without additional allocations.
        private readonly StringBuilder _statsBuilder = new();

        #endregion


        #region Unity Lifecycle

        /// <summary>
        /// Subscribes to Trailback state changes and immediately refreshes the monitor display.
        /// </summary>
        private void OnEnable()
        {
            ModularForge.Trailback.Core.Trailback.OnStateChanged += Refresh;

            var state = ModularForge.Trailback.Core.Trailback.GetState();
            Refresh(state);

            _isMonitorVisible = startVisible;
            
            ToggleRuntimeMonitorVisibility();
        }

        /// <summary>
        /// Unsubscribes from Trailback state changes.
        /// </summary>
        private void OnDisable()
        {
            ModularForge.Trailback.Core.Trailback.OnStateChanged -= Refresh;
        }

        #endregion


        #region Refresh

        /// <summary>
        /// Refreshes all monitor sections using the provided runtime state snapshot.
        /// </summary>
        /// <param name="state">
        /// Current Trailback runtime state.
        /// </param>
        private void Refresh(TrailbackState state)
        {
            UpdateTrailbackState(state);
            UpdateHistoryStats(state);
        }

        #endregion


        #region State Display

        /// <summary>
        /// Updates the navigation state section of the monitor display.
        /// </summary>
        /// <param name="state">
        /// Current Trailback runtime state.
        /// </param>
        private void UpdateTrailbackState(TrailbackState state)
        {
            if (trailbackStateText == null)
            {
                return;
            }

            _stateBuilder.Clear();

            _stateBuilder.AppendLine("TRAILBACK STATE");

            _stateBuilder.AppendLine($"Current: {state.Current}");
            _stateBuilder.AppendLine($"Previous: {state.Previous}");

            _stateBuilder.AppendLine($"Can Go Back: {state.CanGoBack}");

            if (state.BlockReason != BackBlockReason.None)
            {
                _stateBuilder.AppendLine();

                _stateBuilder.AppendLine($"Reason: {state.BlockReason}");

                if (!string.IsNullOrWhiteSpace(state.BlockDetails))
                {
                    _stateBuilder.AppendLine($"Details: {state.BlockDetails}");
                }
            }

            trailbackStateText.text = _stateBuilder.ToString();
        }

        #endregion


        #region Statistics Display

        /// <summary>
        /// Updates the history statistics section of the monitor display.
        /// </summary>
        /// <param name="state">
        /// Current Trailback runtime state.
        /// </param>
        private void UpdateHistoryStats(TrailbackState state)
        {
            if (historyStatsText == null)
            {
                return;
            }

            _statsBuilder.Clear();

            _statsBuilder.AppendLine("HISTORY STATS");

            _statsBuilder.AppendLine($"Categories: {state.ActiveCategoryCount}");
            _statsBuilder.AppendLine($"Total Entries: {state.TotalHistoryEntries}");

            _statsBuilder.AppendLine($"Highest Category: {state.HighestPriorityCategory}");

            historyStatsText.text = _statsBuilder.ToString();
        }

        #endregion

        
        #region Runtime Monitor Visibility
        
        /// <summary>
        /// Canvas containing the Trailback runtime monitor UI.
        /// </summary>
        /// <remarks>
        /// Visibility is controlled through the monitor toggle functionality.
        ///
        /// Disabling the canvas hides diagnostic information without disabling the monitor
        /// component itself.
        /// </remarks>
        [FormerlySerializedAs("runtimeMonitorCanvas")] [SerializeField]
        private Canvas monitorCanvas;

        /// <summary>
        /// Text element used to display the current monitor visibility state.
        /// </summary>
        /// <remarks>
        /// Updated automatically when the runtime monitor visibility changes.
        ///
        /// Example:
        ///
        /// Debug ON
        /// Debug OFF
        /// </remarks>
        [FormerlySerializedAs("toggleButtonStatusText")] [SerializeField]
        private TextMeshProUGUI monitorToggleButtonText;
        
        /// <summary>
        /// Determines whether the runtime monitor is visible when the component is enabled.
        /// </summary>
        /// <remarks>
        /// This value defines the monitor's initial visibility state.
        ///
        /// Runtime visibility changes do not modify this configuration value.
        /// </remarks>
        [SerializeField]
        [Tooltip("Determines whether the runtime monitor is visible when the component is enabled.")]
        private bool startVisible;
        
        /// <summary>
        /// Tracks whether the runtime monitor is currently visible.
        /// </summary>
        /// <remarks>
        /// Used by the monitor toggle workflow to determine the next visibility state.
        /// </remarks>
        private bool _isMonitorVisible;
        
        /// <summary>
        /// Toggles the visibility of the runtime monitor UI.
        /// </summary>
        /// <remarks>
        /// Updates:
        ///
        /// - Monitor canvas visibility
        /// - Toggle button status text
        ///
        /// This method is typically assigned to a UI button for runtime diagnostics control.
        /// </remarks>
        public void ToggleRuntimeMonitorVisibility()
        {
            if (!monitorCanvas)
            {
                Debug.LogWarning("Monitor canvas not assigned.", this);
                return;
            }

            SetMonitorVisibility(_isMonitorVisible);
            UpdateToggleButtonText(_isMonitorVisible);
            
            _isMonitorVisible = !_isMonitorVisible;
        }

        /// <summary>
        /// Sets the runtime monitor visibility state.
        /// </summary>
        /// <param name="visible">
        /// True to show the monitor; otherwise false.
        /// </param>
        private void SetMonitorVisibility(bool visible)
        {
            monitorCanvas.enabled = visible;
        }

        /// <summary>
        /// Updates the toggle button label to reflect the current monitor visibility state.
        /// </summary>
        /// <param name="visible">
        /// Current monitor visibility state.
        /// </param>
        private void UpdateToggleButtonText(bool visible)
        {
            if (!monitorToggleButtonText)
            {
                Debug.LogWarning("Toggle button status text not assigned.");
                return;
            }

            monitorToggleButtonText.text = visible ? "Debug OFF" : "Debug ON";
        }

        #endregion
    }
}