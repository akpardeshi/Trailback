namespace ModularForge.Trailback.Core
{
    /// <summary>
    /// Represents a navigation entry that can be registered in Trailback's navigation history.
    /// </summary>
    public interface IBackNavigable
    {
        /// <summary>
        /// Navigation category that defines how this navigation entry behaves within Trailback's
        /// history system.
        /// </summary>
        NavigationCategorySo NavigationCategory { get; } 
    }
}