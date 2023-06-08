namespace SignalRChat.Entities
{
    public class Message
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime dateTime { get; set; }
        public int ReciverId { get; set; }
        public int UserId { get; set; }
        public User user { get; set; }
    }
}
