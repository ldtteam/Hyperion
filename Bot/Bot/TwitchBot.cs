using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bot.Bot.Handlers;
using Bot.Bot.Interactor;
using Bot.Bot.Senders;
using Bot.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using TwitchLib.Client;
using TwitchLib.Client.Events;
using TwitchLib.Client.Extensions;
using TwitchLib.Client.Models;
using TwitchLib.Communication.Events;

namespace Bot.Bot
{
    /// <inheritdoc />
    /// <summary>
    /// The Twitch platform bot.
    /// </summary>
    public class TwitchBot : IBot
    {
        private readonly ILogger<TwitchBot> _logger;
        private readonly IConfiguration _configuration;

        private readonly IEnumerable<IJoinChannelHandler> _joinChannelHandlers;
        private readonly IEnumerable<IMessageHandler> _messageHandlers;
        private readonly IEnumerable<IWhisperMessageHandler> _whisperMessageHandlers;
        private readonly IEnumerable<ICommandHandler> _commandHandlers;
        private readonly IEnumerable<IWhisperCommandHandler> _whisperCommandHandlers;
        private readonly IEnumerable<IModeratorJoinedHandler> _moderatorJoinedHandlers;
        private readonly IEnumerable<IModeratorLeftHandler> _moderatorLeftHandlers;
        private readonly IEnumerable<IUserJoinedHandler> _userJoinedHandlers;
        private readonly IEnumerable<IUserLeftHandler> _userLeftHandlers;
        
        private TwitchClient _client;

        private readonly Func<TwitchClient, IInteractor> _twitchInteractorBuilder;

        private IInteractor _twitchInteractor;
        
        private readonly List<string> _connectedChannels = new List<string>();

        public TwitchBot(ILogger<TwitchBot> logger, IConfiguration configuration, IEnumerable<IJoinChannelHandler> joinChannelHandlers, IEnumerable<IMessageHandler> messageHandlers, IEnumerable<IWhisperMessageHandler> whisperMessageHandlers, IEnumerable<ICommandHandler> commandHandlers, IEnumerable<IWhisperCommandHandler> whisperCommandHandlers, IEnumerable<IModeratorJoinedHandler> moderatorJoinedHandlers, IEnumerable<IModeratorLeftHandler> moderatorLeftHandlers, IEnumerable<IUserJoinedHandler> userJoinedHandlers, IEnumerable<IUserLeftHandler> userLeftHandlers, Func<TwitchClient, IInteractor> twitchInteractorBuilder)
        {
            _logger = logger;
            _configuration = configuration;
            _joinChannelHandlers = joinChannelHandlers;
            _messageHandlers = messageHandlers;
            _whisperMessageHandlers = whisperMessageHandlers;
            _commandHandlers = commandHandlers;
            _whisperCommandHandlers = whisperCommandHandlers;
            _moderatorJoinedHandlers = moderatorJoinedHandlers;
            _moderatorLeftHandlers = moderatorLeftHandlers;
            _userJoinedHandlers = userJoinedHandlers;
            _userLeftHandlers = userLeftHandlers;
            _twitchInteractorBuilder = twitchInteractorBuilder;

            InitializeClient();
        }

        private void InitializeClient()
        {
            var credentials = new ConnectionCredentials(
                _configuration.GetSection("twitch")["username"],
                _configuration.GetSection("twitch")["password"],
                _configuration.GetSection("twitch")["webSocketUri"],
                _configuration.GetSection("twitch").GetValue<bool>("disableUserNameCheck")
            );
            
            _client = new TwitchClient();
            
            _client.Initialize(credentials);
            
            _client.OnJoinedChannel += OnTwitchJoinChannel;
            _client.OnLeftChannel += OnTwitchLeftChannel;
            
            _client.OnMessageReceived += OnTwitchMessage;
            _client.OnWhisperReceived += OnTwitchWhisperMessage;
            
            _client.OnChatCommandReceived += OnTwitchCommand;
            _client.OnWhisperCommandReceived += OnTwitchWhisperCommand;
            
            _client.OnModeratorJoined += OnTwitchModeratorJoined;
            _client.OnModeratorLeft += OnTwitchModeratorLeft;
            
            _client.OnUserJoined += OnTwitchUserJoined;
            _client.OnUserLeft += OnTwitchUserLeft;

            _client.OnConnected += OnTwitchConnected;
            
            _client.AutoReListenOnException = true;
            _client.OnError += onTwitchError;
        }

        private void OnTwitchLeftChannel(object sender, OnLeftChannelArgs e)
        {
            _logger.LogWarning("Leaving channel! : " + e.Channel);
        }

        private void onTwitchError(object sender, OnErrorEventArgs e)
        {
            _logger.LogError(e.Exception.ToString());
        }

        private void OnTwitchConnected(object sender, OnConnectedArgs e)
        {
            _logger.LogWarning("Connection established!");
            _logger.LogInformation("Joining initial channels");
            _configuration.GetSection("twitch:initialChannels").Get<List<string>>()
                .ForEach((channel => this.JoinChannel(channel)));
            _logger.LogInformation("Joined initial channels");
            
            _twitchInteractor = _twitchInteractorBuilder(_client);
        }

        private void OnTwitchUserLeft(object sender, OnUserLeftArgs e)
        {
            if (e.Username == _configuration.GetSection("twitch")["username"])
                return;
            
            _logger.LogInformation("Processing user left event of: {User} from Channel {Channel}", e.Username, e.Channel);

            foreach (var userLeftHandler in _userLeftHandlers)
            {
                if (userLeftHandler.ProcessEvent(_twitchInteractor, e.Username, e.Channel))
                {
                    _logger.LogInformation("Processed user left event of {User} from Channel {Channel}, with handler {UserLeftHandler}", e.Username, e.Channel, userLeftHandler.GetType().Name);
                    return;
                }
            }
            
            _logger.LogWarning("Processing user left event of {User} from Channel {Channel} failed, no handler was available.", e.Username, e.Channel);
        }

        private void OnTwitchUserJoined(object sender, OnUserJoinedArgs e)
        {
            if (e.Username == _configuration.GetSection("twitch")["username"])
                return;
            
            _logger.LogInformation("Processing user joined event of: {User} from Channel {Channel}", e.Username, e.Channel);

            foreach (var userJoinedHandler in _userJoinedHandlers)
            {
                if (userJoinedHandler.ProcessEvent(_twitchInteractor, e.Username, e.Channel))
                {
                    _logger.LogInformation("Processed user joined event of {User} from Channel {Channel}, with handler {UserJoinedHandler}", e.Username, e.Channel, userJoinedHandler.GetType().Name);
                    return;
                }
            }
            
            _logger.LogWarning("Processing user joined event of {User} from Channel {Channel} failed, no handler was available.", e.Username, e.Channel);
        }

        private void OnTwitchModeratorLeft(object sender, OnModeratorLeftArgs e)
        {
            if (e.Username == _configuration.GetSection("twitch")["username"])
                return;
            
            _logger.LogInformation("Processing moderator left event of: {Moderator} from Channel {Channel}", e.Username, e.Channel);

            foreach (var moderatorLeftHandler in _moderatorLeftHandlers)
            {
                if (moderatorLeftHandler.ProcessEvent(_twitchInteractor, e.Username, e.Channel))
                {
                    _logger.LogInformation("Processed moderator left event of {Moderator} from Channel {Channel}, with handler {ModeratorLeftHandler}", e.Username, e.Channel, moderatorLeftHandler.GetType().Name);
                    return;
                }
            }
            
            _logger.LogWarning("Processing moderator left event of {Moderator} from Channel {Channel} failed, no handler was available.", e.Username, e.Channel);
        }

        private void OnTwitchWhisperCommand(object sender, OnWhisperCommandReceivedArgs e)
        {
            if (e.Command.WhisperMessage.Username == _configuration.GetSection("twitch")["username"])
                return;

            _logger.LogInformation("Processing whisper command: {WhisperCommandMessage} from User: {WhisperUserName}", e.Command.WhisperMessage.Message, e.Command.WhisperMessage.Username);
            
            foreach (var commandHandler in _whisperCommandHandlers)
            {
                if (commandHandler.ProcessCommand(_twitchInteractor, e.Command.WhisperMessage.Username, e.Command.CommandText, e.Command.ArgumentsAsList))
                {
                    _logger.LogInformation(
                        "Processed whisper command: {WhisperCommandMessage} using handler: {WhisperCommandHandlerName} from User {WhisperUsername}", e.Command.WhisperMessage.Message, commandHandler.GetType().Name, e.Command.WhisperMessage.Username);
                    return;
                }
            }
            
            _logger.LogWarning("Failed to process whisper command: {WhisperCommandMessage} from User: {WhisperUserName}, no whisper command handler available!", e.Command.WhisperMessage.Message, e.Command.WhisperMessage.Username);
        }

        private void OnTwitchCommand(object sender, OnChatCommandReceivedArgs e)
        {
            if (e.Command.ChatMessage.Username == _configuration.GetSection("twitch")["username"])
                return;

            _logger.LogInformation("Processing command: {CommandMessagea} from Channel {Channel}", e.Command.ChatMessage.Message, e.Command.ChatMessage.Channel);
            
            foreach (var commandHandler in _commandHandlers)
            {
                if (commandHandler.ProcessCommand(_twitchInteractor, e.Command.ChatMessage.Username, e.Command.ChatMessage.Channel, e.Command.CommandText, e.Command.ArgumentsAsList))
                {
                    _logger.LogInformation(
                        "Processed command: {CommandMessage} using handler: {CommandHandlerName} from Channel {Channel}", e.Command.ChatMessage.Message, commandHandler.GetType().Name, e.Command.ChatMessage.Channel);
                    return;
                }
            }
            
            _logger.LogWarning("Failed to process command: {CommandMessage} from Channel {Channel}, no handler available!", e.Command.ChatMessage.Message, e.Command.ChatMessage.Channel);
        }

        private void OnTwitchWhisperMessage(object sender, OnWhisperReceivedArgs e)
        {
            if (e.WhisperMessage.Username == _configuration.GetSection("twitch")["username"])
                return;

            _logger.LogInformation("Processing whisper message: {WhisperMessage} for username: {UsernameWhispered}", e.WhisperMessage.Message, e.WhisperMessage.Username);

            foreach (var whisperMessageHandler in _whisperMessageHandlers)
            {
                if (whisperMessageHandler.ProcessEvent(_twitchInteractor, e.WhisperMessage.Message, e.WhisperMessage.Username))
                {
                    _logger.LogInformation(
                        "Successfully processed whisper message: {WhisperMessage} for username: {UsernameWhispered}, with handler {WhisperMessageHandler}",
                        e.WhisperMessage.Message, e.WhisperMessage.Username, whisperMessageHandler.GetType().Name);
                    return;
                }
            }

            _logger.LogWarning("Failed to process whisper message: {WhisperMessage} for username: {UsernameWhispered}, no handler available!",
                e.WhisperMessage.Message, e.WhisperMessage.Username);
        }

        private void OnTwitchJoinChannel(object sender, OnJoinedChannelArgs e)
        {
/*            _logger.LogInformation("Processing joined channel event for channel: {ChannelJoined}", e.Channel);

            foreach (var joinChannelHandler in _joinChannelHandlers)
            {
                if (joinChannelHandler.ProcessEvent(_twitchInteractor, e.Channel))
                {
                    _logger.LogInformation("Successfully processed joined channel event for channel: {ChannelJoined}, using handler {ChannelJoinedHandler}", joinChannelHandler.GetType().Name);
                    return;
                }
            }
            
            _logger.LogWarning("Failed to process joined channel event for channel: {ChannelJoined}, no handler available!", e.Channel);*/

            foreach (var clientJoinedChannel in _client.JoinedChannels)
            {
                _logger.LogWarning("Joined: " + clientJoinedChannel.Channel);
            }
        }

        private void OnTwitchMessage(object sender, OnMessageReceivedArgs e)
        {
            if (e.ChatMessage.Username == _configuration.GetSection("twitch")["username"])
                return;

            _logger.LogInformation("Processing chat message: {ChatMessage} for channel: {Channel} from {Username}", e.ChatMessage.Message, e.ChatMessage.Channel, e.ChatMessage.Username);

            foreach (var chatMessageHandler in _messageHandlers)
            {
                if (chatMessageHandler.ProcessEvent(_twitchInteractor, e.ChatMessage.Message, e.ChatMessage.Username, e.ChatMessage.Channel))
                {
                    _logger.LogInformation(
                        "Successfully processed chat message: {ChatMessage} for channel: {Channel} from {Username}, with handler {ChatMessageHandler}",
                        e.ChatMessage.Message, e.ChatMessage.Channel, e.ChatMessage.Username, chatMessageHandler.GetType().Name);
                    return;
                }
            }

            _logger.LogWarning("Failed to process chat message: {ChatMessage} for channel: {Channel} from {Username}, no handler available!",
                e.ChatMessage.Message, e.ChatMessage.Channel, e.ChatMessage.Username);
        }
        
        private void OnTwitchModeratorJoined(object sender, OnModeratorJoinedArgs e)
        {
            if (e.Username == _configuration.GetSection("twitch")["username"])
                return;

            _logger.LogInformation("Processing moderator joined event of: {Moderator} from Channel {Channel}", e.Username, e.Channel);

            foreach (var moderatorJoinedHandler in _moderatorJoinedHandlers)
            {
                if (moderatorJoinedHandler.ProcessEvent(_twitchInteractor, e.Username, e.Channel))
                {
                    _logger.LogInformation("Processed moderator joined event of {Moderator} from Channel {Channel}, with handler {ModeratorJoinedHandler}", e.Username, e.Channel, moderatorJoinedHandler.GetType().Name);
                    return;
                }
            }
            
            _logger.LogWarning("Processing moderator joined event of {Moderator} from Channel {Channel} failed, no handler was available.", e.Username, e.Channel);
        }

        public async Task StartBot()
        {
            _logger.LogWarning("Starting twitch bot.");
            _logger.LogInformation("Connecting to twitch.");
            _client.Connect();
            _logger.LogInformation("Connection created.");
            _logger.LogWarning("Started twitch bot successfully");

            
        }

        public Task JoinChannel(string channelName)
        {
            _logger.LogInformation("Attempting to join channel: {ChannelToJoin}", channelName);
            if (channelName is null || !channelName.Any())
                throw new ArgumentNullException();
            
            if (_connectedChannels.Contains(channelName))
                return Task.CompletedTask;
            
            _logger.LogInformation("Joining channel: {ChannelToJoin}", channelName);
            _client.JoinChannel(channelName);
            
            _connectedChannels.Add(channelName);
            _logger.LogWarning("Joined channel: {ChannelToJoin} successfully", channelName);
            
            return Task.CompletedTask;
        }

        public Task LeaveChannel(string channelName)
        {
            _logger.LogInformation("Attempting to leave channel: {ChannelToLeave}", channelName);
            if (channelName is null || !channelName.Any())
                throw new ArgumentNullException();
            
            if (!_connectedChannels.Contains(channelName))
                throw new ArgumentOutOfRangeException();
            
            _logger.LogInformation("Joining channel: {ChannelToLeave}", channelName);
            _client.JoinChannel(channelName);
            
            _connectedChannels.Add(channelName);
            _logger.LogWarning("Left channel: {ChannelToLeave} successfully", channelName);

            return Task.CompletedTask;
        }
        
        public async Task StopBot()
        {
            _logger.LogWarning("Stopping twitch bot.");
            _logger.LogInformation("Leaving channels");
            await _connectedChannels.ForEachAsync(LeaveChannel);
            _logger.LogInformation("Left all channels");
            _logger.LogInformation("Disconnecting from twitch");
            _client.Disconnect();
            _logger.LogInformation("Disconnected form twitch successfully");
            _logger.LogWarning("Shutdown twitch bot gracefully.");

            //Clear out the senders.
            _twitchInteractor = null;
        }
    }
}