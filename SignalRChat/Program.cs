using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using SignalRChat.Abstractions;
using SignalRChat.Entities;
using SignalRChat.Hubs;
using SignalRChat.Services;

namespace SignalRChat
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();


            builder.Services.AddSingleton<IUserIdProvider, CustomUserIdProvider>(); // Устанавливаем сервис для получения Id пользователя

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.ExpireTimeSpan = TimeSpan.FromHours(0.5);
                    options.AccessDeniedPath = new PathString("/Authentication/AuthenticateUser");
                    options.LoginPath = new PathString("/Authentication/AuthenticateUser");
                });
            builder.Services.AddAuthorization();


            var connectionString = builder.Configuration.GetConnectionString("Default");
            //dependency Injection DataBase
            builder.Services.AddDbContext<SignalRChatContext>(optionsBuilder => optionsBuilder.UseSqlServer(connectionString)); 

            //dependency Injection AutoMapper
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            ////Dependency Injection Services
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<ImessageService, MessageService>();

            builder.Services.AddSignalR();
             
            var app = builder.Build();
             
            

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();   // добавление middleware аутентификации 
            app.UseAuthorization();   // добавление middleware авторизации 

             

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Authentication}/{action=AuthenticateUser}/{id?}");


            app.MapHub<ChatHub>("/chat");

            app.Run();

           
        }
    }
}