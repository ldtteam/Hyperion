using Bot.Bot.Interactor;
using Bot.Bot.Senders;

namespace Bot.Bot.Handlers
{
    public interface IModeratorLeftHandler
    {
        bool ProcessEvent(IInteractor interactor, string username, string channel);
    }
}