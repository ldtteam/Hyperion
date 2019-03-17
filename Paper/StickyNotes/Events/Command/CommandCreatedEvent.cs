using System;
using System.Collections.Generic;
using StickyNotes.Projections.Command;
using StickyNotes.Projections.Platform;

namespace StickyNotes.Events.Command
{
    /// <summary>
    /// Event triggered when a command is created.
    /// </summary>
    public class CommandCreatedEvent
    {
        public CommandCreatedEvent(Guid id, string channelName, IReadOnlyList<SupportedPlatform> platformIdentifier, IReadOnlyList<Guid> triggers, Guid action)
        {
            Id = id;
            ChannelName = channelName;
            PlatformIdentifier = platformIdentifier;
            Triggers = triggers;
            Action = action;
        }

        public CommandCreatedEvent()
        {
        }

        public Guid Id { get; }
        
        public string ChannelName { get; }
        
        public IReadOnlyList<SupportedPlatform> PlatformIdentifier { get; }
        
        public IReadOnlyList<Guid> Triggers { get; }
        
        public Guid Action { get; }
    }
}