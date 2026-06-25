using System;
using UnityEngine;
using UnityEngine.InputSystem;

using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Utilities;

namespace ModularForge.Trailback.Input
{
    /// <summary>
    /// Back input provider that uses Unity's Input System package.
    /// </summary>
    /// <remarks>
    /// Listens for performed events from an assigned InputAction and converts them into back
    /// navigation requests.
    ///
    /// This component allows Trailback to integrate with Unity's Input System without introducing
    /// a direct dependency between the framework and input handling code.
    /// </remarks>
    public sealed class InputSystemBackInputSource : BackInputSource
    {
        #region Configuration

        [Tooltip("Input System action that triggers a back navigation request when performed.")]
        [SerializeField]
        private InputActionReference backAction;
        
        #endregion
        
        
        #region Unity Lifecycle

        /// <summary>
        /// Subscribes to the configured input action when the component becomes active.
        /// </summary>
        /// <remarks>
        /// If no input action has been assigned, the component remains inactive.
        /// </remarks>
        private void OnEnable()
        {
            if (backAction == null)
            {
                return;
            }

            backAction.action.performed += OnBackPerformed;
        }

        /// <summary>
        /// Unsubscribes from the configured input action when the component becomes inactive.
        /// </summary>
        private void OnDisable()
        {
            if (backAction == null)
            {
                return;
            }

            backAction.action.performed -= OnBackPerformed;
        }

        #endregion
        
        
        #region Input Handling
        
        /// <summary>
        /// Handles performed input actions and raises a back navigation request.
        /// </summary>
        /// <param name="context">
        /// Input System callback context associated with the performed action.
        /// </param>
        private void OnBackPerformed(InputAction.CallbackContext context)
        {
            RaiseBackRequested();
        }
        
        #endregion
    }
}