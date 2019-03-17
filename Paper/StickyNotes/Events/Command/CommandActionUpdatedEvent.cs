using System;
using StickyNotes.Projections.Command;

namespace StickyNotes.Events.Command
{
    /// <summary>
    /// Event triggered when a commands action is updated.
    /// </summary>
    public class CommandActionUpdatedEvent
    {
        public CommandActionUpdatedEvent(Guid commandId, Guid action)
        {
            CommandId = commandId;
            Action = action;
        }

        public CommandActionUpdatedEvent()
        {
        }

        public Guid CommandId { get; }
        
        public Guid Action { get; }
    }
}