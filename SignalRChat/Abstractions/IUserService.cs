using SignalRChat.Entities;

namespace SignalRChat.Abstractions
{
    public interface IUserService
    {
        public Task<User> GetUserByNameAsync(string name);
        public Task<int> AddUserASync(User user);
    }
}
