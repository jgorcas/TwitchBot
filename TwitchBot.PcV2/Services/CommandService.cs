using System.Text.RegularExpressions;
using Serilog;
using TwitchBot.PcV2.Interfaces;
using TwitchLib.Client.Events;
using TwitchLib.Client.Models;

namespace TwitchBot.PcV2.Services
{
    public class CommandService : ICommandService
    {
        private readonly ITwitchClientService _twitchClientService;
        private readonly ILogger _logger;

        public CommandService(ITwitchClientService twitchClientService, ILogger logger)
        {
            _twitchClientService = twitchClientService;
            _logger = logger;
            _twitchClientService.GetTwitchClient().OnMessageReceived += OnOnMessageReceived;
        }

        private void OnOnMessageReceived(object? sender, OnMessageReceivedArgs e)
        {
            PlayCommand(e.ChatMessage);
            SayHello(e.ChatMessage);
        }

        private Regex rgxSayHello = new Regex(@"^.*(bonjour|hello|salut|hi|yo)\s?.*", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        private void SayHello(ChatMessage chatMessage)
        {
            if(rgxSayHello.IsMatch(chatMessage.Message))
                _twitchClientService.SendMessage(chatMessage.Channel,$"Bonjour {chatMessage.Username} !! Comment vas tu ?");
        }

        private void PlayCommand(ChatMessage chatMessage)
        {
            if(!chatMessage.Message.StartsWith('!')) return;
            string message = chatMessage.Message[1..].ToLower();
            string result;
            switch (message)
            {
                case "commands":
                    result = "Commandes existantes : !commands | !git | !github | !roll";
                    break;
                case "git":
                case "github":
                    result = "Lien Github : http://www.github.com/jgorcas";
                    break;
                case "roll":
                    Random rdn = new Random(DateTime.Now.Millisecond);
                    result = $"Roll 1-100 => {rdn.Next(1, 100)}";
                    break;
                default:
                    return;
            }
            _twitchClientService.SendMessage(chatMessage.Channel,result);


        }




    }
}