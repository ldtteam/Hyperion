using System;
using System.Threading.Tasks;

namespace Bot.Bot
{
    /// <summary>
    /// Represents the actual bot.
    ///
    /// Handles:
    ///   - Building a connection to a platform.
    ///   - Connecting to a Channel
    ///   - Disconnection from a Channel
    ///   - Tearing down the connection with the platform
    /// </summary>
    public interface IBot
    {
        /// <summary>
        /// Starts the bot.
        /// 
        /// Creates a connection with a platform and waits for further instructions.
        /// </summary>
        /// <returns>The task indicating the start of the bot.</returns>
        Task StartBot();

        /// <summary>
        /// Makes the bot join a given channel.
        /// </summary>
        /// <param name="channelName">The channel to join.</param>
        /// <exception cref="ArgumentNullException">Thrown when the channel parameter is null or empty.</exception>
        /// <exception cref="ArgumentException">Thrown when the channel does not exist.</exception>
        /// <exception cref="AccessViolationException">Thrown when the bot could not join the given channel.</exception>
        /// <returns>The task indicating the joining of the channel</returns>
        Task JoinChannel(string channelName);

        /// <summary>
        /// Makes the bot leave a given channel.
        /// </summary>
        /// <param name="channelName">The channel to leave.</param>
        /// <exception cref="ArgumentNullException">Thrown when the channel parameter is null or empty.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the bot is not connected to the given channel.</exception>
        /// <returns>The task indicating the leaving of the channel</returns>
        Task LeaveChannel(string channelName);

        /// <summary>
        /// Stops the bot.
        ///
        /// Leaves all channels and disconnected from the platform.
        /// </summary>
        /// <returns>The task indicating the stopping of the bot.</returns>
        Task StopBot();
    }
}