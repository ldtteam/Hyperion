using System;
using System.Collections.Generic;
using StickyNotes.Projections.Platform;

namespace StickyNotes.Events.Command.Trigger
{
    public class CommandTriggerCreated
    {
        public CommandTriggerCreated(Guid commandTriggerId, IReadOnlyCollection<Guid> inputProcessors, string cronDefinition, PlatformEvent platformEvent)
        {
            CommandTriggerId = commandTriggerId;
            InputProcessors = inputProcessors;
            CronDefinition = cronDefinition;
            PlatformEvent = platformEvent;
        }

        public CommandTriggerCreated()
        {
        }

        public Guid CommandTriggerId;
        
        public IReadOnlyCollection<Guid> InputProcessors { get; }
        
        public string CronDefinition { get; }
        
        public PlatformEvent PlatformEvent { get; }
    }
}