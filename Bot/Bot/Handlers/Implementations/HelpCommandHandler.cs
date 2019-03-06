using System.Collections.Generic;
using Bot.Bot.Interactor;
using Bot.Bot.Senders;

namespace Bot.Bot.Handlers.Implementations
{
    public class HelpCommandHandler : ICommandHandler
    {
        public bool ProcessCommand(IInteractor interactor, string username, string chatMessageChannel,
            string commandName, List<string> commandParameters)
        {
            if (commandName == "help")
            {
                interactor.MessageDeletionHandler.deleteLastMessage(username, chatMessageChannel);
                interactor.MessageSender.sendMessage("Hyperion bot is in testing. I do not know what todo yet!", chatMessageChannel);
                return true;
            }

            return false;
        }
    }
}