using System;

namespace StickyNotes.Events.Command.Trigger
{
    public class CommandTriggerDeleted
    {
        public CommandTriggerDeleted(Guid commandTriggerId)
        {
            CommandTriggerId = commandTriggerId;
        }

        public CommandTriggerDeleted()
        {
        }

        public Guid CommandTriggerId { get; }
    }
}