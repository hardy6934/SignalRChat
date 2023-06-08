using Microsoft.EntityFrameworkCore;
using SignalRChat.Abstractions;
using SignalRChat.Entities;

namespace SignalRChat.Services
{
    public class UserService : IUserService
    {
        private readonly SignalRChatContext context;
        public UserService(SignalRChatContext context)
        {
            this.context = context;

        }
        public async Task<User>? GetUserByNameAsync(string name)
        {
            var user = await context.users.FirstOrDefaultAsync(x => x.Name.Equals(name));
            return user;
        }

        public async Task<int> AddUserASync(User user)
        {
            await context.users.AddAsync(user);
            return context.SaveChanges();
        }
    }
}
