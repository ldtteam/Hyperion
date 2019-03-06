using Bot.Bot.Interactor;
using Bot.Bot.Senders;

namespace Bot.Bot.Handlers
{
    public interface IJoinChannelHandler
    {
        bool ProcessEvent(IInteractor interactor, string channel);
    }
}