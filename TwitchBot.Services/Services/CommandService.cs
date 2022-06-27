using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Serilog;
using TwitchBot.Services.Interfaces;
using TwitchBot.Services.Models;
using TwitchLib.Client.Events;
using TwitchLib.Client.Models;

namespace TwitchBot.Services.Services
{
    public class CommandService : ICommandService
    {
        private readonly ITwitchClientService _twitchClientService;
        private readonly ILogger _logger;
        private readonly TextCommand[] _textCommands;

        public CommandService(ITwitchClientService twitchClientService, ILogger logger)
        {
            _twitchClientService = twitchClientService;
            _logger = logger;
            _twitchClientService.GetTwitchClient().OnMessageReceived += OnOnMessageReceived;
            var json = File.ReadAllText(@"TwitchTextCommands.json");
            _textCommands = JsonConvert.DeserializeObject<TextCommand[]>(json) ?? Array.Empty<TextCommand>();
        }

        private void OnOnMessageReceived(object? sender, OnMessageReceivedArgs e)
        {
            PlayCommand(e.ChatMessage);
            SayHello(e.ChatMessage);
        }

        private readonly Regex _rgxSayHello = new(@"\b(bonjour|hello|hi|salut)\b", RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Singleline);

        private void SayHello(ChatMessage chatMessage)
        {
            if(_rgxSayHello.IsMatch(chatMessage.Message))
                _twitchClientService.SendMessage(chatMessage.Channel,$"Bonjour {chatMessage.Username} !! Comment vas tu ?");
        }

        private void PlayCommand(ChatMessage chatMessage)
        {
            if(!chatMessage.Message.StartsWith('!')) return;


            var textCommand = _textCommands.FirstOrDefault(tc =>
                tc.Command.Equals(chatMessage.Message, StringComparison.OrdinalIgnoreCase));
            if(textCommand!=null)
            {
                _twitchClientService.SendMessage(chatMessage.Channel, textCommand.Message);
                return;
            }


            string message = chatMessage.Message[1..].ToLower();
            string result;
            switch (message)
            {
                case "commands":
                    result = $"Commandes existantes : !commands | !roll | { string.Join( " | ", _textCommands.Select(t => t.Command))}";
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