namespace TwitchBot.PoC
{
    class Program
    {
        public static void Main(string[] args)
        {
            Bot client = new Bot();
            client.Connect();
            Console.ReadKey();
            client.Disconnect();
            Console.ReadKey();
        }
    }
}