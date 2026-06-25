using UnityEngine;

namespace ModularForge.Trailback.Input
{
    /// <summary>
    /// Back input provider that uses Unity's legacy Input Manager.
    /// </summary>
    /// <remarks>
    /// Monitors a configured keyboard key and converts key presses into back navigation
    /// requests.
    ///
    /// This component provides a lightweight alternative to InputSystemBackInputSource
    /// for projects using Unity's legacy input system.
    /// </remarks>
    public sealed class LegacyBackInputSource : BackInputSource
    {
        [Tooltip("Pressing this key raises a back navigation request.")] [SerializeField]
        private KeyCode backKey = KeyCode.Escape;

        /// <summary>
        /// Monitors the configured key and raises a back navigation request when pressed.
        /// </summary>
        private void Update()
        {
            if (UnityEngine.Input.GetKeyDown(backKey))
            {
                RaiseBackRequested();
            }
        }
    }
}