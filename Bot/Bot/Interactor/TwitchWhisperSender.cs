using Microsoft.Extensions.Logging;
using TwitchLib.Client;

namespace Bot.Bot.Senders
{
    public class TwitchWhisperSender : IWhisperSender
    {
        private readonly ILogger<TwitchWhisperSender> _logger;
        private readonly TwitchClient _client;

        public TwitchWhisperSender(ILogger<TwitchWhisperSender> logger, TwitchClient client)
        {
            _logger = logger;
            _client = client;
        }

        public void sendWhisper(string message, string username)
        {
            _logger.LogInformation("W> {WhisperToUsername}: {WhisperToMessage}", username, message);
            //_client.SendWhisper(username, message);
        }
    }
}