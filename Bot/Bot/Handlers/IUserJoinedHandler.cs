using Bot.Bot.Interactor;
using Bot.Bot.Senders;

namespace Bot.Bot.Handlers
{
    public interface IUserJoinedHandler
    {
        bool ProcessEvent(IInteractor interactor, string username, string channel);
    }
}