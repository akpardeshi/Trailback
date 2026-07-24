namespace ModularForge.Trailback.Core
{
    /// <summary>
    /// Defines how Trailback handles attempts to register a navigation entry that already exists in the
    /// navigation history.
    /// </summary>
    public enum DuplicatePolicy
    {
        /// <summary>
        /// Allows duplicate navigation entries to be added to the navigation history.
        /// </summary>
        Allow = 0,
        
        /// <summary>
        /// Ignores registration requests for navigation entries that already exist in the history.
        /// </summary>
        Ignore = 1,
    }
}