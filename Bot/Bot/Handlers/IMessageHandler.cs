using Bot.Bot.Interactor;
using Bot.Bot.Senders;

namespace Bot.Bot.Handlers
{
    /// <summary>
    /// Represents a handler for messages.
    /// If the handler successfully deals with the message, no further processing on any
    /// handlers will be made.
    /// </summary>
    public interface IMessageHandler
    {
        bool ProcessEvent(IInteractor interactor, string chatMessageMessage, string chatMessageUsername, string chatMessageChannel);
    }
}