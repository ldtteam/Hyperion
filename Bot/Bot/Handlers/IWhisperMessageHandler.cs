using Bot.Bot.Interactor;
using Bot.Bot.Senders;

namespace Bot.Bot.Handlers
{
    public interface IWhisperMessageHandler
    {
        bool ProcessEvent(IInteractor interactor, string whisperMessageMessage, string whisperMessageUsername);
    }
}