using System;
using ModularForge.Trailback.Core;

namespace ModularForge.Trailback.Integration
{
    /// <summary>
    /// Defines the integration contract used to communicate with Trailback.
    /// </summary>
    /// <remarks>
    /// TrailbackIntegrationBridge acts as an abstraction layer between application code and the Trailback
    /// framework.
    ///
    /// Applications should depend on this bridge rather than referencing Trailback directly.
    ///
    /// Concrete implementations are responsible for translating bridge operations into Trailback
    /// framework calls.
    /// </remarks>
    public abstract class TrailbackIntegrationBridge
    {
        
        #region Navigation Visibility
        
        /// <summary>
        /// Registers a navigation entry as visible.
        /// </summary>
        /// <param name="element">
        /// Navigation entry that became visible.
        /// </param>
        public abstract void Show(IBackNavigable element);

        /// <summary>
        /// Registers a navigation entry as hidden.
        /// </summary>
        /// <param name="element">
        /// Navigation entry that became hidden.
        /// </param>
        public abstract void Hide(IBackNavigable element);
        
        #endregion
        
        
        #region Navigation

        /// <summary>
        /// Attempts to perform a back navigation operation.
        /// </summary>
        /// <returns>
        /// True if navigation was executed successfully, otherwise false.
        /// </returns>
        public abstract bool Back();
        
        #endregion
        
        
        #region History

        /// <summary>
        /// Removes all navigation history.
        /// </summary>
        public abstract void ResetHistory();
        
        #endregion

        
        #region Events
        
        /// <summary>
        /// Raised when navigation reaches the root of the history stack and no further back
        /// navigation can be performed.
        /// </summary>
        public abstract event Action RootReached;

        #endregion

    }
}