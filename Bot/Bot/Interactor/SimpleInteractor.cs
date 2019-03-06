using Bot.Bot.Senders;

namespace Bot.Bot.Interactor
{
    public class SimpleInteractor : IInteractor
    {
        public SimpleInteractor(IMessageSender messageSender, IWhisperSender whisperSender, IMessageDeletionHandler messageDeletionHandler)
        {
            MessageSender = messageSender;
            WhisperSender = whisperSender;
            MessageDeletionHandler = messageDeletionHandler;
        }

        public IMessageSender MessageSender { get; }
        public IWhisperSender WhisperSender { get; }
        public IMessageDeletionHandler MessageDeletionHandler { get; }
    }
}