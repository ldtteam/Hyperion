using System;
using System.Threading;
using System.Threading.Tasks;
using Bot.Bot;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Bot.Service
{
    public class BotHostedService : IHostedService
    {
        private ILogger<BotHostedService> _logger;
        private IBot _bot;
        
        public BotHostedService(
            ILogger<BotHostedService> logger, IBot bot)
        {
            _logger = logger;
            _bot = bot;
        }
        
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting Hosted Bot.");

            await _bot.StartBot();
            
            _logger.LogInformation("Bot started.");
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Stopping Hosted Bot.");

            await _bot.StopBot();
            
            _logger.LogInformation("Bot ended.");
        }
    }
}