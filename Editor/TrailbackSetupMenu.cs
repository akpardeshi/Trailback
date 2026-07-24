using ModularForge.Trailback.Core;
using UnityEditor;
using UnityEngine;

namespace ModularForge.Trailback.Editor
{
    /// <summary>
    /// Provides Unity Editor menu commands for creating and configuring Trailback components in the current scene.
    /// </summary>
    public static class TrailbackSetupMenu
    {
        #region Constants

        /// <summary>
        /// Root menu path used for all Trailback setup commands.
        /// </summary>
        private const string MenuRoot = "GameObject/Trailback/";

        #endregion


        #region Menu Item Methods
        
        /// <summary>
        /// Creates a GameObject configured with a
        /// <see cref="TrailbackEventListener"/> component.
        /// </summary>
        [MenuItem(MenuRoot + "Create Event Listener", false, 12)]
        private static void CreateEventListener()
        {
            var go = new GameObject("Trailback Event Listener");
            go.AddComponent<TrailbackEventListener>();
            Select(go);
        }

        #endregion


        #region Utility Methods

        /// <summary>
        /// Registers an undo operation and selects the specified GameObject in the hierarchy.
        /// </summary>
        /// <param name="target">
        /// GameObject to select.
        /// </param>
        /// <remarks>
        /// Registers an undo operation to support standard Unity editor workflows.
        /// </remarks>
        private static void Select(GameObject target)
        {
            Undo.RegisterCreatedObjectUndo(target, "Create Trailback Object");
            Selection.activeGameObject = target;
        }
        
        #endregion
    }
}