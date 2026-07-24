namespace ModularForge.Trailback.Core
{
    /// <summary>
    /// Defines whether a back navigation request should be allowed or blocked.
    /// </summary>
    public enum BackNavigationMode
    {
        /// <summary>
        /// Allows the back navigation request to proceed normally.
        /// </summary>
        Allow = 0,
        
        
        /// <summary>
        /// Prevents Trailback from executing the back navigation request.
        /// </summary>
        Block = 1,
    }
}