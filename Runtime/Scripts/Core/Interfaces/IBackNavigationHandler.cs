namespace ModularForge.Trailback.Core
{
    /// <summary>
    /// Executes navigation when Trailback resolves a back navigation request.
    /// </summary>
    /// <remarks>
    /// Trailback is responsible for history management and navigation resolution, but does not directly
    /// control how navigation is performed.
    ///
    /// Implementations determine how the current and previous navigation entries should be handled
    /// when a back navigation operation occurs.
    /// </remarks>
    public interface IBackNavigationHandler
    {
        /// <summary>
        /// Executes navigation using the resolved navigation context.
        /// </summary>
        /// <param name="context">
        /// Contains the current and previous navigation entries involved in the navigation operation.
        /// </param>
        void NavigateBackTo(BackContext context);
    }
}