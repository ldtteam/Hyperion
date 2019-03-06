using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Bot.Bot.Interactor;

namespace Bot.Bot.Handlers.Implementations
{
    public class WinsCommandHandler : ICommandHandler
    {
        private ConcurrentDictionary<string, int> _wins = new ConcurrentDictionary<string, int>();
        
        public bool ProcessCommand(IInteractor interactor, string username, string chatMessageChannel, string commandName,
            List<string> commandParameters)
        {
            var combinedCommand = commandName + " " + commandParameters.Aggregate((s1, s2) => s1 + " " + s2);
            
            var currentWins = _wins.GetOrAdd(chatMessageChannel, 0);

            if (combinedCommand.StartsWith("wins"))
            {
                if (combinedCommand.Replace(" ", "").Equals("wins++")){
                    currentWins++;
                    _wins.TryAdd(chatMessageChannel, currentWins);                    
                }

                if (combinedCommand.Replace(" ", "").Equals("wins-reset"))
                {
                    currentWins = 0;
                    _wins.TryAdd(chatMessageChannel, currentWins);
                }
                
                interactor.MessageSender.sendMessage($"Currently @{chatMessageChannel} has {currentWins} booyah!", chatMessageChannel);
                return true;
            }

            return false;
        }
    }
}