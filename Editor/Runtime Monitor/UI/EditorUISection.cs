using ModularForge.Trailback.Editor.Styling;
using UnityEngine.UIElements;

namespace ModularForge.Trailback.Editor
{
    /// <summary>
    /// Represents a reusable section within the Trailback Editor UI.
    /// A section consists of a styled header and a content container.
    /// </summary>
    public class EditorUISection : VisualElement
    {
        #region Properties

        /// <summary>
        /// Gets the visual container where section content should be added.
        /// </summary>
        public VisualElement Content { get; }

        #endregion


        #region Initialization

        /// <summary>
        ///  Initializes a new Editor UI section with a styled header and content container.
        /// </summary>
        /// <param name="title">The section title.</param>
        public EditorUISection(string title)
        {
            style.marginBottom = 12;

            var header = new Label(title);
            TrailbackEditorUIStyles.StyleSectionHeader(header);
            Add(header);

            Content = new VisualElement();
            Content.style.marginTop = 4;
            Content.style.marginLeft = 8;

            Add(Content);
        }

        #endregion
    }
}