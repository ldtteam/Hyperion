using System;
using Microsoft.Extensions.Logging;
using TwitchLib.Client;
using TwitchLib.Client.Extensions;

namespace Bot.Bot.Interactor
{
    public class TwitchMessageDeletionHandler : IMessageDeletionHandler
    {
        private ILogger<TwitchMessageDeletionHandler> _logger;
        private TwitchClient _client;

        public TwitchMessageDeletionHandler(ILogger<TwitchMessageDeletionHandler> logger, TwitchClient client)
        {
            _logger = logger;
            _client = client;
        }

        public void deleteLastMessage(string username, string channel)
        {
            _logger.LogInformation("Deleting last message of: {DeletionOfUsername}, in channel {DeletionOfChannel}", username, channel);
            //_client.TimeoutUser(channel, username, TimeSpan.FromSeconds(1));
        }
    }
}