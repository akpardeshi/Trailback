using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace ModularForge.Trailback.Demo.Editor
{
    public static class DemoInputSwitcher
    {
        [MenuItem("Tools/Trailback/Demo/Use Legacy Input")]
        private static void UseLegacyInput()
        {
            SwitchInput(useLegacy: true);
        }

        [MenuItem("Tools/Trailback/Demo/Use Unity Input System")]
        private static void UseUnityInputSystem()
        {
            SwitchInput(useLegacy: false);
        }

        private static void SwitchInput(bool useLegacy)
        {
            var controller = Object.FindAnyObjectByType<DemoNavigationController>();
            var legacy = Object.FindAnyObjectByType<LegacyBackInputSource>(FindObjectsInactive.Include);
            var inputSystem = Object.FindAnyObjectByType<InputSystemBackInputSource>(FindObjectsInactive.Include);

            if (controller == null || legacy == null || inputSystem == null)
            {
                EditorUtility.DisplayDialog("Trailback Demo", "Could not locate all required demo components.", "OK");

                return;
            }

            // Begin one Undo operation
            Undo.IncrementCurrentGroup();
            Undo.SetCurrentGroupName("Switch Trailback Input");

            Undo.RecordObject(controller, "Switch Trailback Input");
            Undo.RecordObject(legacy, "Switch Trailback Input");
            Undo.RecordObject(inputSystem, "Switch Trailback Input");

            // Apply changes
            legacy.enabled = useLegacy;
            inputSystem.enabled = !useLegacy;

            controller.SetBackInputSource(useLegacy ? legacy : inputSystem);

            // Mark modified objects dirty
            EditorUtility.SetDirty(controller);
            EditorUtility.SetDirty(legacy);
            EditorUtility.SetDirty(inputSystem);

            // Mark scene dirty
            EditorSceneManager.MarkSceneDirty(controller.gameObject.scene);

            // Collapse into one Ctrl+Z operation
            Undo.CollapseUndoOperations(Undo.GetCurrentGroup());

            Debug.Log(useLegacy ? "Switched demo to Legacy Input." : "Switched demo to Unity Input System.");
        }
    }
}