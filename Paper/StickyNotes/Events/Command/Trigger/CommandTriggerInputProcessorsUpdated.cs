using System;
using System.Collections.Generic;
using StickyNotes.Projections.Command;

namespace StickyNotes.Events.Command.Trigger
{
    public class CommandTriggerInputProcessorsUpdated
    {
        public CommandTriggerInputProcessorsUpdated(Guid commandTriggerId, IReadOnlyList<CommandInputProcessor> inputProcessors)
        {
            CommandTriggerId = commandTriggerId;
            InputProcessors = inputProcessors;
        }

        public CommandTriggerInputProcessorsUpdated()
        {
        }

        public Guid CommandTriggerId { get; }

        public IReadOnlyList<CommandInputProcessor> InputProcessors { get; }
    }
}