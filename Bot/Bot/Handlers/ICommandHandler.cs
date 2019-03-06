using System.Collections.Generic;
using Bot.Bot.Interactor;
using Bot.Bot.Senders;

namespace Bot.Bot.Handlers
{
    public interface ICommandHandler
    {
        bool ProcessCommand(IInteractor interactor, string username, string chatMessageChannel, string commandName, List<string> commandParameters);
    }
}