using System;
using System.Collections.Generic;
using StickyNotes.Projections.Platform;

namespace StickyNotes.Projections.Command
{
    public class CommandTrigger : AbstractAggragate
    {
        private IReadOnlyList<Guid> _inputProcessors;
        private string _cronDefintion;
        private PlatformEvent _platformEvent;

        public CommandTrigger(Guid id, IReadOnlyList<Guid> inputProcessors, string cronDefintion, PlatformEvent platformEvent)
        {
            Id = id;
            _inputProcessors = inputProcessors;
            _cronDefintion = cronDefintion;
            _platformEvent = platformEvent;
        }

        public CommandTrigger()
        {
        }

        public IReadOnlyList<Guid> InputProcessors
        {
            get => _inputProcessors;
            set => _inputProcessors = value;
        }

        public string CronDefintion
        {
            get => _cronDefintion;
            set => _cronDefintion = value;
        }

        public PlatformEvent PlatformEvent
        {
            get => _platformEvent;
            set => _platformEvent = value;
        }
    }
    
}