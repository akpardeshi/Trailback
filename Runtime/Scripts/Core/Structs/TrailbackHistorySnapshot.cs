using System.Collections.Generic;

namespace ModularForge.Trailback.Core
{
    /// <summary>
    /// Represents an immutable snapshot of Trailback's navigation history.
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
    public readonly struct TrailbackHistorySnapshot
    {
        /// <summary>
        /// Ordered collection of navigation history entries captured by this snapshot.
        /// </summary>
        public IReadOnlyList<TrailbackHistoryEntry> Entries { get; }

        /// <summary>
        /// Creates a new immutable history snapshot.
        /// </summary>
        /// <param name="entries">
        /// Ordered collection of navigation history entries captured for this snapshot.
        /// </param>
        internal TrailbackHistorySnapshot(IReadOnlyList<TrailbackHistoryEntry> entries)
        {
            Entries = entries;
        }
    }
}