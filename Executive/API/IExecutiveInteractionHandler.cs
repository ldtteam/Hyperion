using Executive.Implementation;

namespace Executive.API
{
    /// <summary>
    /// Represents the executives possible interactions with its external environment.
    /// Allows for replies and system commands to be executed during execution of an <see cref="Action"/>.
    ///
    /// Its implementation is highly dependent on the running platform.
    /// If any of these methods are not available on an implementing platform they should NOOP.
    /// </summary>
    public interface IExecutiveInteractionHandler
    {

        /// <summary>
        /// Method called by the sourcecode of an <see cref="Action"/> to sent a reply into the same source,
        /// as the receiving of the message which resulted in a command being executed.
        /// </summary>
        /// <param name="message">The message to sent as a reply.</param>
        void Reply(string message);

        /// <summary>
        /// Method called by the sourcecode of an <see cref="Action"/> to sent a reply into the a given private message channel
        /// of a given username.
        /// </summary>
        /// <param name="username">The username to send the private message to.</param>
        /// <param name="message">The message to send</param>
        void SendPersonalMessage(string username, string message);

        /// <summary>
        /// Method called by the sourcecode of an <see cref="Action"/> to sent timeout a user for a given amount of seconds.
        /// </summary>
        /// <param name="username">The username to timeout.</param>
        /// <param name="seconds">The length of time in seconds to timeout.</param>
        void TimeOutUser(string username, int seconds);

        /// <summary>
        /// Method called by the sourcecode of an <see cref="Action"/> to ban a given user.
        /// </summary>
        /// <param name="username">The user to ban.</param>
        /// <param name="message">The message to send to the user that is about to be banned.</param>
        void BanUser(string username, string message);

        /// <summary>
        /// Method called by the sourcecode of an <see cref="Action"/> to unban a given user.
        /// </summary>
        /// <param name="username">The user to unban.</param>
        void UnbanUser(string username);
    }
}