namespace ModularForge.Trailback.Core
{
    /// <summary>
    /// Represents the navigation context resolved for a back navigation operation.
    /// </summary>
    /// <remarks>
    /// Contains the active navigation entry and the navigation entry that will become active after
    /// navigation is executed.
    ///
    /// This context is produced by BackResolver and consumed by IBackNavigationHandler.
    /// </remarks>
    public readonly struct BackContext
    {
        /// <summary>
        /// The currently active navigation entry.
        /// </summary>
        public IBackNavigable Current { get; }

        /// <summary>
        /// The navigation entry that will become active after navigation is executed.
        /// </summary>
        public IBackNavigable Previous { get; }

        /// <summary>
        /// Creates a new navigation context.
        /// </summary>
        /// <param name="current">
        /// The currently active navigation entry.
        /// </param>
        /// <param name="previous">
        /// The navigation entry that will become active after navigation is executed.
        /// </param>
        public BackContext(IBackNavigable current, IBackNavigable previous)
        {
            Current = current;
            Previous = previous;
        }
    }
}