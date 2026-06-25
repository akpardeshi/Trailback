namespace ModularForge.Trailback.Diagnostics
{
    /// <summary>
    /// Describes why a back navigation request cannot currently be executed.
    /// </summary>
    /// <remarks>
    /// BackBlockReason is primarily used by Trailback's diagnostic systems and runtime
    /// monitoring tools.
    ///
    /// Values are exposed through TrailbackState and may be displayed by debugging or
    /// monitoring interfaces.
    /// </remarks>
    public enum BackBlockReason
    {
        /// <summary>
        /// No navigation block is currently active.
        /// </summary>
        None = 0,
        
        /// <summary>
        /// Navigation cannot continue because the navigation root has been reached.
        /// </summary>
        RootReached = 1,
        
        /// <summary>
        /// Navigation is blocked by the active navigation entry.
        ///
        /// Typically occurs when the active entry implements IBackNavigationBlocker and
        /// prevents navigation from continuing.
        /// </summary>
        BlockedByConfiguration = 2,
        
        /// <summary>
        /// Navigation cannot continue because no valid history entries are available.
        /// </summary>
        NoHistoryAvailable = 3
    }
}