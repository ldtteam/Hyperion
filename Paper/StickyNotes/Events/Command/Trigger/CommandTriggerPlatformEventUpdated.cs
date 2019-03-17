using System;
using StickyNotes.Projections.Platform;

namespace StickyNotes.Events.Command.Trigger
{
    public class CommandTriggerPlatformEventUpdated
    {
        public CommandTriggerPlatformEventUpdated(Guid commandTriggerId, PlatformEvent platformEvent)
        {
            CommandTriggerId = commandTriggerId;
            PlatformEvent = platformEvent;
        }

        public CommandTriggerPlatformEventUpdated()
        {
        }

        public Guid CommandTriggerId { get; }

        public PlatformEvent PlatformEvent { get; }
    }
}