using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SignalRChat.Abstractions;
using SignalRChat.Entities;
using SignalRChat.Models;

namespace SignalRChat.Services
{
    public class MessageService : ImessageService
    {
        private readonly SignalRChatContext context;
        private readonly IMapper mapper;
        public MessageService(SignalRChatContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;

        } 
        public async Task<int> CreateMessageAsync(Message message)
        {
            if (message != null )
            {
                await context.messages.AddAsync(message);
                return await context.SaveChangesAsync(); 
            }
            return 0;
        }

        public async Task<List<MessageModel>> GetMessagesHistoryForUsersAsync(int SenderId, int ReciverId)
        {
            var messages =  await context.messages.Where(x => x.UserId.Equals(SenderId) && x.ReciverId.Equals(ReciverId) || x.UserId.Equals(ReciverId) && x.ReciverId.Equals(SenderId))
                .Select(x=>x).Include(x=>x.user).OrderBy(x=>x.dateTime).ToListAsync();

            var messageModels = messages.Select(x => mapper.Map<MessageModel>(x)).ToList();
            return messageModels;
        }
    }
}
    