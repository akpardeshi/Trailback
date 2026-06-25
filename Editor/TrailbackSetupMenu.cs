using ModularForge.Trailback.Input;
using ModularForge.Trailback.Integration;
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
        /// <see cref="LegacyBackInputSource"/> component.
        /// </summary>
        [MenuItem(MenuRoot + "Create Legacy Input Source", false, 10)]
        private static void CreateLegacyInputSource()
        {
            var go = new GameObject("Legacy Back Input Source");
            go.AddComponent<LegacyBackInputSource>();
            Select(go);
        }

        /// <summary>
        /// Creates a GameObject configured with an
        /// <see cref="InputSystemBackInputSource"/> component.
        /// </summary>
        [MenuItem(MenuRoot + "Create Input System Source", false, 11)]
        private static void CreateInputSystemSource()
        {
            var go = new GameObject("Input System Back Input Source");
            go.AddComponent<InputSystemBackInputSource>();
            Select(go);
        }

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

        /// <summary>
        /// Creates a Runtime Monitor instance from the configured monitor prefab.
        /// </summary>
        /// <remarks>
        /// The monitor provides runtime diagnostics for navigation history, categories, blockers,
        /// and navigation state.
        /// </remarks>
        [MenuItem(MenuRoot + "Create Runtime Monitor", false, 13)]
        private static void CreateRuntimeMonitor()
        {
            GameObject instance = CreateRuntimeMonitorInstance();

            if (instance == null)
            {
                return;
            }

            Selection.activeGameObject = instance;
        }
        

        /// <summary>
        /// Creates a complete Trailback setup in the current scene.
        /// </summary>
        /// <remarks>
        /// The setup includes:
        ///
        /// - Legacy Back Input Source
        /// - Trailback Event Listener
        /// - Runtime Monitor
        ///
        /// All objects are organized beneath a single Trailback root GameObject.
        ///
        /// Additional application-specific integration is still required before navigation will
        /// function.
        /// </remarks>
        [MenuItem(MenuRoot + "Create Complete Setup", false, 100)]
        private static void CreateCompleteSetup()
        {
            var root = new GameObject("Trailback");

            var input = new GameObject("Legacy Back Input Source");
            var listener = new GameObject("Trailback Event Listener");

            input.transform.SetParent(root.transform);
            listener.transform.SetParent(root.transform);

            input.AddComponent<LegacyBackInputSource>();
            listener.AddComponent<TrailbackEventListener>();

            CreateRuntimeMonitorInstance(root.transform);

            Select(root);
        }

        #endregion
        
        
        #region Creation Methods

        /// <summary>
        /// Creates a Runtime Monitor instance from the configured monitor prefab.
        /// </summary>
        /// <param name="parent">
        /// Optional parent transform used to organize the created monitor within the hierarchy.
        /// </param>
        /// <returns>
        /// The instantiated Runtime Monitor instance, or null if creation failed.
        /// </returns>
        private static GameObject CreateRuntimeMonitorInstance(Transform parent = null)
        {
            GameObject instance = InstantiatePrefab(TrailbackAssetPaths.RuntimeMonitorPrefab);

            if (instance == null)
            {
                Debug.LogWarning("[Trailback] Failed to create Runtime Monitor.");

                return null;
            }

            if (parent != null)
            {
                instance.transform.SetParent(parent, false);
            }

            Undo.RegisterCreatedObjectUndo(instance, "Create Trailback Runtime Monitor");

            return instance;
        }
        
        #endregion
        

        #region Utility Methods

        /// <summary>
        /// Registers an undo operation and selects the specified GameObject in the hierarchy.
        /// </summary>
        /// <param name="target">
        /// GameObject to select.
        /// </param>
        /// /// <remarks>
        /// Registers an undo operation to support standard Unity editor workflows.
        /// </remarks>
        private static void Select(GameObject target)
        {
            Undo.RegisterCreatedObjectUndo(target, "Create Trailback Object");
            Selection.activeGameObject = target;
        }

        /// <summary>
        /// Instantiates a prefab from the specified asset path.
        /// </summary>
        /// <param name="prefabPath">
        /// Asset path of the prefab to instantiate.
        /// </param>
        /// <returns>
        /// The instantiated prefab instance, or null if the prefab could not be loaded.
        /// </returns>
        private static GameObject InstantiatePrefab(string prefabPath)
        {
            GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath);

            if (prefab == null)
            {
                Debug.LogWarning($"[Trailback] Prefab not found." + $" Path: {prefabPath}");
                return null;
            }

            return (GameObject)PrefabUtility.InstantiatePrefab(prefab);
        }

        #endregion
    }
}