using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace SignalRChat.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
       
        public async Task Send(string title, string message, string to)
        {
             
            if (Context.UserIdentifier is string userName)
            {
                await Clients.Users(to, userName).SendAsync("Receive", title, message, userName);
            }
        }

        public override async Task OnConnectedAsync()
        {
            await Clients.Caller.SendAsync("Notify", Context.UserIdentifier);
            await base.OnConnectedAsync();
        }

    }
}
