using System;
using System.Collections.Generic;
using StickyNotes.Projections.Command;

namespace StickyNotes.Events.Command
{
    /// <summary>
    /// Event triggered when a command has its trigger collection updated.
    /// </summary>
    public class CommandTriggersUpdatedEvent
    {
        public CommandTriggersUpdatedEvent(Guid commandId, IReadOnlyList<Guid> triggers)
        {
            CommandId = commandId;
            Triggers = triggers;
        }

        public CommandTriggersUpdatedEvent()
        {
        }

        public Guid CommandId { get; }
        
        public IReadOnlyList<Guid> Triggers { get; }
    }
}