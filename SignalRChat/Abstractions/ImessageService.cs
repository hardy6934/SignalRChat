using SignalRChat.Entities;
using SignalRChat.Models;

namespace SignalRChat.Abstractions
{
    public interface ImessageService
    {
        Task<int> CreateMessageAsync(Message message);
        Task<List<MessageModel>> GetMessagesHistoryForUsersAsync(int SenderId, int ReciverId);

    }
}
