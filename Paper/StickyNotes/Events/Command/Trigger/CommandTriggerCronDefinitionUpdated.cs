using System;

namespace StickyNotes.Events.Command.Trigger
{
    public class CommandTriggerCronDefinitionUpdated
    {
        public CommandTriggerCronDefinitionUpdated(Guid commandTriggerId, string cronDefintion)
        {
            CommandTriggerId = commandTriggerId;
            CronDefintion = cronDefintion;
        }

        public CommandTriggerCronDefinitionUpdated()
        {
        }

        public Guid CommandTriggerId { get; }
        
        public string CronDefintion { get; }
    }
}