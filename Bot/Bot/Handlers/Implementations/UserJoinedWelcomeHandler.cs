using Bot.Bot.Interactor;
using Bot.Bot.Senders;

namespace Bot.Bot.Handlers.Implementations
{
    public class UserJoinedWelcomeHandler : IUserJoinedHandler
    {
        public bool ProcessEvent(IInteractor interactor, string username, string channel)
        {
            interactor.WhisperSender.sendWhisper($"Welcome to {channel}'s channel. Please remember to adhere to the chat rules!", username);
            return true;
        }
    }
}