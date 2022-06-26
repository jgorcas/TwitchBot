namespace TwitchBot.PcClient.Models
{
    public sealed class UserMessage
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime CreatedDate { get; set; }

        public override string ToString()
        {
            return $"{CreatedDate} - {Text}";
        }
    }
}
