using System;
using Microsoft.Extensions.Logging;
using TwitchLib.Client;

namespace Bot.Bot.Senders
{
    public class TwitchMessageSender : IMessageSender
    {
        private readonly ILogger<TwitchMessageSender> _logger;
        private readonly TwitchClient _client;

        public TwitchMessageSender(ILogger<TwitchMessageSender> logger, TwitchClient client)
        {
            _logger = logger;
            _client = client;
        }

        public void sendMessage(string message, string channel)
        {
            _logger.LogInformation("M> {MessageToChannel}: {MessageToMessage}", channel, message);
            try
            {
                _client.SendMessage(channel, message);
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to send message!");
            }
        }
    }
}