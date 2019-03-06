namespace Bot.Bot.Senders
{
    public interface IWhisperSender
    {
        void sendWhisper(string message, string username);
    }
}