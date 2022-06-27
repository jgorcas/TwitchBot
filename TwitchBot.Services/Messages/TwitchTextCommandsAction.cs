using Newtonsoft.Json;
using TwitchBot.Services.Models;
using TwitchLib.Client;
using TwitchLib.Client.Models;

namespace TwitchBot.Services.Messages
{

    public sealed class TwitchTextCommandsAction : IMessageAction
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

        public void RunAction(TwitchClient client,ChatMessage chatMessage)
        {
            client.SendMessage(chatMessage.Channel,_textCommands.First(tc => tc.Command.Equals(chatMessage.Message, StringComparison.OrdinalIgnoreCase)).Message);
        }
    }
}