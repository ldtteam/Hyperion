using System;
using System.Collections.Generic;
using StickyNotes.Projections.Platform;

namespace StickyNotes.Events.Command
{
    public class CommandSupportedPlatformUpdatedEvent
    {
        public CommandSupportedPlatformUpdatedEvent(Guid commandId, IReadOnlyList<SupportedPlatform> supportedPlatforms)
        {
            CommandId = commandId;
            SupportedPlatforms = supportedPlatforms;
        }

        public CommandSupportedPlatformUpdatedEvent()
        {
        }

        public Guid CommandId { get; }
        
        public IReadOnlyList<SupportedPlatform> SupportedPlatforms { get; set; }
    }
}