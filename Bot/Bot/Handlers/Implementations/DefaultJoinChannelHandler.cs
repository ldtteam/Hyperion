using Bot.Bot.Interactor;
using Bot.Bot.Senders;

namespace Bot.Bot.Handlers.Implementations
{
    public class DefaultJoinChannelHandler : IJoinChannelHandler
    {
        public bool ProcessEvent(IInteractor interactor, string channel)
        {
            interactor.MessageSender.sendMessage("Hyperion bot is now lurking in the shadows!", channel);
            return true;
        }
    }
}