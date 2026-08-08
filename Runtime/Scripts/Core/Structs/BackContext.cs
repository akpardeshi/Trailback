namespace ModularForge.Trailback.Core
{
    /// <summary>
    /// Represents the navigation context resolved for a back navigation operation.
    /// </summary>
    /// <remarks>
    /// Represents an immutable snapshot of Trailback's navigation history at a
    /// specific point in time.
    ///
    /// Each navigation entry is represented by a <see cref="TrailbackHistoryEntry"/>,
    /// providing a stable view of the history for diagnostics, the Runtime Monitor,
    /// debugging, and other developer tools without exposing the mutable runtime state.
    ///
    /// Because the snapshot is immutable, it does not reflect changes made to the
    /// navigation history after it has been created.
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
        public IBackNavigable BackTarget { get; }

        /// <summary>
        /// Creates a new navigation context.
        /// </summary>
        /// <param name="current">
        /// The currently active navigation entry.
        /// </param>
        /// <param name="backTarget">
        /// The navigation entry that will become active after navigation is executed.
        /// </param>
        public BackContext(IBackNavigable current, IBackNavigable backTarget)
        {
            Current = current;
            BackTarget = backTarget;
        }
    }
}