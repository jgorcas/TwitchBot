using Newtonsoft.Json;
using TwitchBot.Services.Interfaces;
using TwitchBot.Services.Models;
using TwitchLib.Client.Interfaces;
using TwitchLib.Client.Models;

namespace TwitchBot.Services.Services.CommandServiceAction
{

    public sealed class TwitchTextCommandsAction : ICommandServiceAction
    {
        private readonly TextCommand[] _textCommands;
        public TwitchTextCommandsAction()
        {
            var json = File.ReadAllText(@"TwitchTextCommands.json");
             _textCommands = JsonConvert.DeserializeObject<TextCommand[]>(json)?? Array.Empty<TextCommand>();
        }

        public bool IsConcern(string command)
        {
            return _textCommands.Any(tc => tc.Command.Equals(command, StringComparison.OrdinalIgnoreCase));
        }

        public void RunAction(ITwitchClient client,ChatMessage chatMessage)
        {
            client.SendMessage(chatMessage.Channel,_textCommands.First(tc => tc.Command.Equals(chatMessage.Message, StringComparison.OrdinalIgnoreCase)).Message);
        }
    }
}