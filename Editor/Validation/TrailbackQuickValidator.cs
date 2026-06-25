using ModularForge.Trailback.Diagnostics;
using ModularForge.Trailback.Input;
using ModularForge.Trailback.Integration;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ModularForge.Trailback.Editor
{
    /// <summary>
    /// Validates the current scene for common Trailback setup issues such as duplicate components,
    /// disabled objects, and missing optional tools.
    /// </summary>
    public static class TrailbackQuickValidator
    {
        #region Variables

        /// <summary>
        /// Unity Editor menu path used to execute Trailback setup validation.
        /// </summary>
        private const string ValidateMenuPath = "Tools/Trailback/Quick Setup Validation";

        #endregion


        #region Public Methods

        /// <summary>
        /// Executes all Trailback setup validation checks and logs the results to the Unity Console.
        /// </summary>
        [MenuItem(ValidateMenuPath)]
        public static void Validate()
        {
            ValidationResult result = new ValidationResult();

            ValidateMissingComponent<EventSystem>(result, ValidationSeverity.Warning, "EventSystem");
            ValidateDuplicateComponents<EventSystem>(result, "EventSystem");
            ValidateDisabledComponents<EventSystem>(result, ValidationSeverity.Warning, "EventSystem");

            ValidateMissingComponent<BackInputSource>(result, ValidationSeverity.Error, "BackInputSource");
            ValidateDuplicateComponents<BackInputSource>(result, "BackInputSource");
            ValidateDisabledComponents<BackInputSource>(result, ValidationSeverity.Warning, "BackInputSource");

            ValidateMissingComponent<TrailbackEventListener>(result, ValidationSeverity.Info, "TrailbackEventListener");
            ValidateDuplicateComponents<TrailbackEventListener>(result, "TrailbackEventListener");
            ValidateDisabledComponents<TrailbackEventListener>(result, ValidationSeverity.Warning, "TrailbackEventListener");

            ValidateMissingComponent<TrailbackMonitorView>(result, ValidationSeverity.Info, "Runtime Monitor");
            ValidateDuplicateComponents<TrailbackMonitorView>(result, "Runtime Monitor");
            ValidateDisabledComponents<TrailbackMonitorView>(result, ValidationSeverity.Warning, "Runtime Monitor");

            string report = ValidationReportFormatter.Format(result);

            if (result.HasErrors)
            {
                Debug.LogError(report);
            }

            else if (result.HasWarnings)
            {
                Debug.LogWarning(report);
            }

            else
            {
                Debug.Log(report);
            }
        }

        #endregion


        #region Validation Methods

        /// <summary>
        /// Detects duplicate components of the specified type and records a validation warning.
        /// </summary>
        /// <typeparam name="T">
        /// Component type to validate.
        /// </typeparam>
        /// <param name="result">
        /// Validation result being populated.
        /// </param>
        /// <param name="componentName">
        /// Display name used in validation messages.
        /// </param>
        private static void ValidateDuplicateComponents<T>(ValidationResult result, string componentName)
            where T : Component
        {
            var components = Object.FindObjectsByType<T>(FindObjectsInactive.Include, FindObjectsSortMode.None);

            if (components.Length <= 1)
            {
                return;
            }

            result.Add(ValidationSeverity.Warning, $"Multiple {componentName}s found ({components.Length}).");

            foreach (var component in components)
            {
                LogValidationObject($"Duplicate {componentName}:", component);
            }
        }

        /// <summary>
        /// Detects disabled components of the specified type and records a validation message.
        /// </summary>
        /// <typeparam name="T">
        /// Component type to validate.
        /// </typeparam>
        /// <param name="result">
        /// Validation result being populated.
        /// </param>
        /// <param name="severity">
        /// Severity level assigned to the validation message.
        /// </param>
        /// <param name="componentName">
        /// Display name used in validation messages.
        /// </param>
        private static void ValidateDisabledComponents<T>(ValidationResult result, ValidationSeverity severity,
            string componentName) where T : Behaviour
        {
            var components = Object.FindObjectsByType<T>(FindObjectsInactive.Include, FindObjectsSortMode.None);

            foreach (var component in components)
            {
                if (component.isActiveAndEnabled)
                {
                    continue;
                }

                result.Add(severity, $"{componentName} disabled: {component.name}", component);

                LogValidationObject($"Disabled {componentName}:", component);
            }
        }

        /// <summary>
        /// Validates that at least one component of the specified type exists in the current scene.
        /// </summary>
        /// <typeparam name="T">
        /// Component type to validate.
        /// </typeparam>
        /// <param name="result">
        /// Validation result being populated.
        /// </param>
        /// <param name="severity">
        /// Severity level assigned to the validation message.
        /// </param>
        /// <param name="componentName">
        /// Display name used in validation messages.
        /// </param>
        private static void ValidateMissingComponent<T>(ValidationResult result, ValidationSeverity severity,
            string componentName) where T : Component
        {
            var components = Object.FindObjectsByType<T>(FindObjectsInactive.Include, FindObjectsSortMode.None);

            if (components.Length > 0)
            {
                return;
            }

            result.Add(severity, $"No {componentName} found in the scene.");
        }

        #endregion


        #region Logging Methods

        /// <summary>
        /// Logs a clickable validation message that references a specific scene object.
        /// </summary>
        /// <param name="message">
        /// Validation message to display.
        /// </param>
        /// <param name="context">
        /// Scene object associated with the message.
        /// </param>
        private static void LogValidationObject(string message, Object context)
        {
            Debug.LogWarning($"[Trailback Validation] {message} {context.name}", context);
        }

        #endregion
    }
}