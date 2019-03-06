using Bot.Bot.Interactor;
using Bot.Bot.Senders;

namespace Bot.Bot.Handlers
{
    public interface IUserLeftHandler
    {
        bool ProcessEvent(IInteractor interactor, string username, string channel);
    }
}