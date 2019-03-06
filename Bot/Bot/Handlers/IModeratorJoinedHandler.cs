using Bot.Bot.Interactor;
using Bot.Bot.Senders;

namespace Bot.Bot.Handlers
{
    public interface IModeratorJoinedHandler
    {
        bool ProcessEvent(IInteractor interactor, string username, string channel);
    }
}