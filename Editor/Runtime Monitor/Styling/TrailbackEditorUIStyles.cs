using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace ModularForge.Trailback.Editor.Styling
{
    /// <summary>
    /// Provides reusable UI Toolkit styling helpers for the Trailback Editor UI.
    /// </summary>
    public static class TrailbackEditorUIStyles
    {
        #region Layout

        /// <summary>
        /// Applies the common layout styling used by the Runtime Editor UI.
        /// </summary>
        public static void ApplyRoot(VisualElement root)
        {
            root.style.paddingLeft = 12;
            root.style.paddingRight = 12;
            root.style.paddingTop = 12;
            root.style.paddingBottom = 12;

            root.style.flexGrow = 1;
        }

        #endregion


        #region Sections

        /// <summary>
        /// Applies the standard styling for section headers.
        /// </summary>
        public static void StyleSectionHeader(Label label)
        {
            label.style.unityFontStyleAndWeight = FontStyle.Bold;
            label.style.fontSize = 13;
            label.style.marginBottom = 4;
        }

        #endregion


        #region Fields

        /// <summary>
        /// Creates a bold field label used for property names.
        /// </summary>
        public static Label CreateFieldLabel(string text)
        {
            var label = new Label(text);

            StyleFieldLabel(label);

            return label;
        }

        /// <summary>
        /// Applies the standard styling used for field labels in the Editor UI.
        /// </summary>
        private static void StyleFieldLabel(Label label)
        {
            label.style.unityFontStyleAndWeight = FontStyle.Bold;
            // Indent values beneath their corresponding field label.
            label.style.fontSize = 12;
            label.style.marginTop = 8;
            label.style.marginBottom = 2;
        }

        /// <summary>
        /// Creates a value label used to display runtime information.
        /// </summary>
        public static Label CreateValueLabel()
        {
            var label = new Label
            {
                style =
                {
                    marginLeft = 12,
                    marginBottom = 8
                }
            };

            return label;
        }

        #endregion


        #region Separators

        /// <summary>
        /// Creates a horizontal divider used to visually separate sections.
        /// </summary>
        public static VisualElement CreateDivider(
            float height = 1f, 
            float marginTop = 6f, 
            float marginBottom = 8f)
        {
            var divider = new VisualElement
            {
                style =
                {
                    height = height,
                    marginTop = marginTop,
                    marginBottom = marginBottom,
                    backgroundColor = GetDividerColor()
                }
            };

            return divider;
        }

        /// <summary>
        /// Returns the appropriate divider color for the active Unity Editor theme.
        /// </summary>
        private static Color GetDividerColor()
        {
            return EditorGUIUtility.isProSkin 
                ? new Color(0.18f, 0.18f, 0.18f) : 
                new Color(.65f, .65f, .65f);
        }
        
        #endregion
        
        
        #region Ping
        
        private const string PingGlyph = "◉";
        
        /// <summary>
        /// Creates a clickable ping label that highlights a Unity object in the Editor.
        /// </summary>
        public static Label CreatePingLabel()
        {
            var label = new Label(PingGlyph)
            {
                tooltip = "Ping Unity Object",
                style =
                {
                    fontSize = 16,
                    unityFontStyleAndWeight = FontStyle.Bold,
                    unityTextAlign = TextAnchor.MiddleCenter,
                    width = 18,
                    height = 18,
                    marginLeft = 6,
                    alignSelf = Align.Center
                }
            };

            label.RegisterCallback<ClickEvent>(OnPingClicked);

            return label;
        }
        
        /// <summary>
        /// Pings the Unity object associated with the clicked ping label.
        /// </summary>
        private static void OnPingClicked(ClickEvent evt)
        {
            if (evt.currentTarget is not Label label)
            {
                return;
            }

            if (label.userData is Object obj)
            {
                EditorGUIUtility.PingObject(obj);
            }
        }
        
        /// <summary>
        /// Enables or disables interaction with a ping label.
        /// </summary>
        public static void SetPingEnabled(Label label, bool enabled)
        {
            label.style.opacity = enabled ? 1f : 0.7f;
            label.pickingMode = enabled ? PickingMode.Position : PickingMode.Ignore;
        }
        
        #endregion
    }
}