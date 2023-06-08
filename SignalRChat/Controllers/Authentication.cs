using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using SignalRChat.Models;
using System.Security.Claims;
using SignalRChat.Abstractions;
using SignalRChat.Services;
using System;
using SignalRChat.Entities;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;

namespace SignalRChat.Controllers
{
    public class Authentication : Controller
    {
        private readonly IUserService userService;
        private readonly ImessageService messageService;
        private readonly IMapper mapper;


        public Authentication(IUserService userService, ImessageService messageService, IMapper mapper)
        {
            this.userService = userService;
            this.messageService = messageService;
            this.mapper = mapper;
        }

        [HttpGet]
        public IActionResult AuthenticateUser()
        {
            return View("Index");
        }

        [HttpPost]
        public async Task<IActionResult> AuthenticateUserAsync(AuthenticationModel model)
        {
            User user = new User { Name = model.UserName };
            if (model != null)
            { 
                await Authenticate(user.Name);
                return RedirectToAction("ChatView", "Chat");
            }
            return RedirectToAction("AuthenticateUser", "Authentication");
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();

            return RedirectToAction("AuthenticateUser", "Authentication");
        }

        private async Task Authenticate(string userName)
        {
             
            var User = await userService.GetUserByNameAsync(userName);

            if (User == null)
            {
                await userService.AddUserASync(new User { Name = userName});
                User = await userService.GetUserByNameAsync(userName);
            }


            var claims = new List<Claim>(){

                new Claim(ClaimTypes.NameIdentifier, User.Name) 
            }; 

            if (claims != null)
            {
                var identity = new ClaimsIdentity(claims, "ApplicationCookie");

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(identity));
            } 
        }

       
         

    }
}
