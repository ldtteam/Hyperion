namespace Bot.Bot.Interactor
{
    public interface IMessageDeletionHandler
    {
        void deleteLastMessage(string username, string channel);
    }
}