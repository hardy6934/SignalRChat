using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace SignalRChat.Entities
{
    public class SignalRChatContext : DbContext
    {
        public DbSet<Message> messages { get; set; }
        public DbSet<User> users { get; set; }

        public SignalRChatContext(DbContextOptions<SignalRChatContext> options)
            : base(options)
        {

        }
    }
}
