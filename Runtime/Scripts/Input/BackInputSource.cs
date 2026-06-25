using System;
using UnityEngine;
using UnityEngine.Events;

namespace ModularForge.Trailback.Input
{
    /// <summary>
    /// Base class for all back input providers.
    /// </summary>
    /// <remarks>
    /// BackInputSource converts platform-specific or framework-specific input into a unified
    /// back navigation request.
    ///
    /// Implementations are responsible for detecting input and notifying listeners when a back
    /// action occurs.
    ///
    /// This class does not execute navigation and does not depend on Trailback.
    /// </remarks>
    public abstract class BackInputSource : MonoBehaviour
    {
        #region Events
        
        /// <summary>
        /// Raised when a back input request is detected.
        /// </summary>
        /// <remarks>
        /// Subscribe to this event to respond to back navigation requests through code.
        /// </remarks>
        public event Action BackRequested;
        
        
        [SerializeField]
        [Tooltip("Invoked when a back input is detected. Allows designers to respond to back requests directly from the Inspector.")]
        private UnityEvent onBackRequested;
        
        #endregion
        
        
        #region Dispatch

        /// <summary>
        /// Raises a back navigation request.
        /// </summary>
        /// <remarks>
        /// Notifies both code subscribers and Inspector event listeners that a back input has been
        /// detected.
        ///
        /// Derived classes should call this method when their respective input source detects a back
        /// action.
        /// </remarks>
        protected void RaiseBackRequested()
        {
            BackRequested?.Invoke();
            onBackRequested?.Invoke();
        }
        
        #endregion
    }
}