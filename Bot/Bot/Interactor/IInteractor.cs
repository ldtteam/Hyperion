using Bot.Bot.Senders;

namespace Bot.Bot.Interactor
{
    public interface IInteractor
    {
        
        IMessageSender MessageSender { get; }
        
        IWhisperSender WhisperSender { get; }

        IMessageDeletionHandler MessageDeletionHandler { get; }
    }
}