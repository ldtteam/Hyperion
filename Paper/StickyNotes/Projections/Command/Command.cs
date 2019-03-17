using System;
using System.Collections.Generic;
using StickyNotes.Events.Command;
using StickyNotes.Projections.Platform;

namespace StickyNotes.Projections.Command
{
    /// <summary>
    /// A single command to be executed by a hyperion executive executor.
    /// Combines information about the channel, target platform, triggers and the executed action.
    /// </summary>
    public class Command : AbstractAggragate
    {
        private string _channelName;
        private IReadOnlyList<SupportedPlatform> _platformIdentifier;
        private IReadOnlyList<Guid> _triggers;
        private Guid _action;

        public Command(string channelName, IReadOnlyList<SupportedPlatform> platformIdentifier,
            IReadOnlyList<Guid> triggers, Guid? action)
        {
            Id = Guid.NewGuid();
            _channelName = channelName ?? throw new ArgumentNullException(nameof(channelName));
            _platformIdentifier = platformIdentifier ?? throw new ArgumentNullException(nameof(platformIdentifier));
            _triggers = triggers ?? throw new ArgumentNullException(nameof(triggers));
            _action = action ?? throw new ArgumentNullException(nameof(action));
            
            RaiseEvent(new CommandCreatedEvent(Id, _channelName, _platformIdentifier, _triggers, _action));
        }

        public Command()
        {
        }

        public string ChannelName => _channelName;

        public IReadOnlyList<SupportedPlatform> PlatformIdentifier
        {
            get => _platformIdentifier;
            set
            {
                _platformIdentifier = value;
                RaiseEvent(new CommandSupportedPlatformUpdatedEvent(Id, _platformIdentifier));
            }
        }

        public IReadOnlyList<Guid> Triggers
        {
            get => _triggers;
            set
            {
                _triggers = value;
                RaiseEvent(new CommandTriggersUpdatedEvent(Id, _triggers));
            }
        }

        public Guid Action
        {
            get => _action;
            set
            {
                _action = value;
                RaiseEvent(new CommandActionUpdatedEvent(Id, _action));
            }
        }
    }
}