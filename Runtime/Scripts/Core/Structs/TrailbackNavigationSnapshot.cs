namespace ModularForge.Trailback.Core
{
    /// <summary>
    /// Represents Trailback's current navigation state.
    /// </summary>
    /// <remarks>
    /// Represents an immutable snapshot of Trailback's navigation state at a specific
    /// point in time.
    ///
    /// It contains the current navigation entry, the entry that would become active
    /// after a successful back navigation, and additional information describing
    /// whether back navigation can currently be performed.
    ///
    /// This snapshot is intended for diagnostics, the Runtime Monitor, debugging,
    /// and other developer tooling.
    /// </remarks>
    public readonly struct TrailbackNavigationSnapshot
    {
        /// <summary>
        /// Currently active navigation entry.
        /// </summary>
        public TrailbackHistoryEntry Current { get; }

        /// <summary>
        /// Navigation entry that would become active after a successful back navigation.
        /// </summary>
        public TrailbackHistoryEntry BackTarget { get; }

        /// <summary>
        /// Indicates whether back navigation can currently be performed.
        /// </summary>
        public bool CanGoBack { get; }

        /// <summary>
        /// Indicates why navigation is currently blocked.
        /// </summary>
        public BackBlockReason BlockReason { get; }

        /// <summary>
        /// Additional information describing the current block reason.
        /// </summary>
        public string BlockDetails { get; }

        /// <summary>
        /// Creates a new navigation snapshot.
        /// </summary>
        /// <param name="current">
        /// Currently active navigation entry.
        /// </param>
        /// <param name="backTarget">
        /// Navigation entry that would become active after a successful back navigation.
        /// </param>
        /// <param name="canGoBack">
        /// Indicates whether back navigation can currently be performed.
        /// </param>
        /// <param name="blockReason">
        /// Reason navigation is currently blocked.
        /// </param>
        /// <param name="blockDetails">
        /// Additional information describing the current block reason.
        /// </param>
        public TrailbackNavigationSnapshot(
            TrailbackHistoryEntry current,
            TrailbackHistoryEntry backTarget,
            bool canGoBack,
            BackBlockReason blockReason,
            string blockDetails)
        {
            Current = current;
            BackTarget = backTarget;
            CanGoBack = canGoBack;
            BlockReason = blockReason;
            BlockDetails = blockDetails;
        }
    }
}