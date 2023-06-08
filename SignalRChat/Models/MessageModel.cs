using SignalRChat.Entities;

namespace SignalRChat.Models
{
    public class MessageModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime dateTime { get; set; }
        public int ReciverId { get; set; }

        public int UserId { get; set; } 
        public string UserName { get; set; }
    }
}
