using System;

namespace StickyNotes.Events.Command
{
    /// <summary>
    /// Event triggered when a command is deleted.
    /// </summary>
    public class CommandDeletedEvent
    {
        public Guid Id { get; }
    }
}