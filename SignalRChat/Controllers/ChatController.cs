using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SignalRChat.Abstractions;
using SignalRChat.Entities;
using SignalRChat.Services;

namespace SignalRChat.Controllers
{
    [Authorize]
    public class ChatController : Controller
    {
        private readonly IUserService userService;
        private readonly ImessageService messageService;
        private readonly IMapper mapper;


        public ChatController(IUserService userService, ImessageService messageService, IMapper mapper)
        {
            this.userService = userService;
            this.messageService = messageService;
            this.mapper = mapper;
        }
        public IActionResult ChatView()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> CreateMessageAsync(string Title, string Body, string Name, string Reciver)
        {

            if (!string.IsNullOrEmpty(Title) && !string.IsNullOrEmpty(Body) && !string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(Reciver))
            {
                var userSender = await userService.GetUserByNameAsync(Name);
                var UserReciver = await userService.GetUserByNameAsync(Reciver);
                if (UserReciver == null)
                {
                    await userService.AddUserASync(new User { Name = Reciver });
                    UserReciver = await userService.GetUserByNameAsync(Reciver);
                }

                Message message = new()
                {
                    UserId = userSender.Id,
                    dateTime = DateTime.Now,
                    Title = Title,
                    Body = Body,
                    ReciverId = UserReciver.Id

                };

                await messageService.CreateMessageAsync(message);

                return Ok();
            }
            return BadRequest();
        }

        
         
        public async Task<IActionResult> ChatHistoryAsync(string Sender, string Reciver)
        {
            if (await userService.GetUserByNameAsync(Reciver) == null)
            {
                await userService.AddUserASync(new User { Name = Reciver });
            }
            var messagesHistory = await messageService.GetMessagesHistoryForUsersAsync((await userService.GetUserByNameAsync(Sender)).Id, (await userService.GetUserByNameAsync(Reciver)).Id);
            return View(messagesHistory);
        }


    }
}
