namespace Bot.Bot.Senders
{
    public interface IMessageSender
    {
        void sendMessage(string message, string channel);
    }
}