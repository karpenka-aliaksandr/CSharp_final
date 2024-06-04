using MessageApp.Models;

namespace MessageApp.Abstraction
{
    public interface IMessageRepository
    {
        IEnumerable<Message> GetMessageForUser(Guid userId);
        void SendMessage(Message message);
    }
}
