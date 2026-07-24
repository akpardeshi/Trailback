namespace ModularForge.Trailback.Core
{
    /// <summary>
    /// Represents a navigation entry that can prevent Trailback from executing normal back navigation.
    /// </summary>
    /// <remarks>
    /// When the active navigation entry implements this interface and returns <see cref="BackNavigationMode.Block"/>,
    /// Trailback immediately stops processing the current back navigation request.
    ///
    /// This is commonly used by modal dialogs, confirmation popups, onboarding flows,
    /// purchase prompts, or other UI that must remain visible until the user explicitly dismisses it.
    ///
    /// Blocking entries are responsible for providing their own dismissal workflow.
    /// </remarks>
    public interface IBackNavigationBlocker
    {
        /// <summary>
        /// Determines whether the active navigation entry allows or blocks back navigation.
        /// </summary>
        BackNavigationMode BackNavigationMode { get; }
    }
}