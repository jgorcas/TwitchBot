namespace TwitchBot.PcClient.Models
{
    public sealed class User
    {
        public User()
        {
            
        }
        public User(string userName)
        {
            UserName = userName;
            FirstConnectionDate = DateTime.Now;
            LastConnectionDate = DateTime.Now;
        }
        public int Id { get; set; }
        public long? IdTwitch { get; set; }
        public string UserName { get; set; } = string.Empty;
        public DateTime FirstConnectionDate { get; set; } 
        public DateTime? LastConnectionDate { get; set; }
        public DateTime? LastMessageDate { get; set; }
        public List<UserMessage> Messages { get; set; }
    }
}
