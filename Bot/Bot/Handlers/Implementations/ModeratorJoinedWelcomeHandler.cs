using Bot.Bot.Interactor;
using Bot.Bot.Senders;

namespace Bot.Bot.Handlers.Implementations
{
    public class ModeratorJoinedWelcomeHandler : IModeratorJoinedHandler
    {
        public bool ProcessEvent(IInteractor interactor, string username, string channel)
        {
            interactor.WhisperSender.sendWhisper($"Welcome to {channel} LiveStream. Thanks for moderating!", username);
            return true;
        }
    }
}