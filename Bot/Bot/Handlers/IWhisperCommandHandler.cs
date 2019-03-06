using System.Collections.Generic;
using Bot.Bot.Interactor;
using Bot.Bot.Senders;

namespace Bot.Bot.Handlers
{
    public interface IWhisperCommandHandler
    {
        bool ProcessCommand(IInteractor interactor, string whisperMessageUsername, string commandCommandText,
            List<string> commandArgumentsAsList);
    }
}