using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Bot.Bot.Interactor;

namespace Bot.Bot.Handlers.Implementations
{
    public class BlamesCommandHandler : ICommandHandler
    {
        private ConcurrentDictionary<string, ConcurrentDictionary<string, int>> _blames = new ConcurrentDictionary<string, ConcurrentDictionary<string, int>>();
        
        public bool ProcessCommand(IInteractor interactor, string username, string chatMessageChannel, string commandName,
            List<string> commandParameters)
        {
            var combinedCommand = commandName;
            if (commandParameters.Any())
            {
                combinedCommand += " " + commandParameters.Aggregate((s1, s2) => s1 + " " + s2);
            }
             

            var currentBlames = _blames.GetOrAdd(chatMessageChannel, new ConcurrentDictionary<string, int>());

            if (combinedCommand.StartsWith("blame"))
            {
                var nameCandidate = combinedCommand.Replace("blame-", "").Replace("++", "").Replace("-reset", "").Trim();
                var currentBlameCount = currentBlames.GetOrAdd(nameCandidate, 0);

                if (combinedCommand.EndsWith("++"))
                {
                    currentBlameCount++;
                }
                else if (combinedCommand.EndsWith("-reset"))
                {
                    currentBlameCount = 0;
                }

                currentBlames.TryAdd(nameCandidate, currentBlameCount);
                
                interactor.MessageSender.sendMessage($"Blame! Blame! Blame! @{nameCandidate} has {currentBlameCount} Blames!", chatMessageChannel);
                return true;
            }

            return false;
        }
    }
}