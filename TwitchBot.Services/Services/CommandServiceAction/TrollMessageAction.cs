using System.Text.RegularExpressions;
using TwitchBot.Services.Interfaces;
using TwitchLib.Client.Interfaces;
using TwitchLib.Client.Models;

namespace TwitchBot.Services.Services.CommandServiceAction
{
    public class TrollMessageAction : ICommandServiceAction
    {
        private readonly Dictionary<string, string> _keyWordResponse = new()
        {
            {"chat","meow"},
            {"chats","meow meow"},

        };
        public bool IsConcern(string message)
        {
            return _keyWordResponse.Keys.Select(word => new Regex($@"\b{word}\b", RegexOptions.IgnoreCase)).Any(rgx => rgx.IsMatch(message));
        }

        public void RunAction(ITwitchClient client, ChatMessage chatMessage)
        {
            foreach (var word in _keyWordResponse.Keys)
            {
                Regex rgx = new Regex($@"\b{word}\b", RegexOptions.IgnoreCase);
                if (rgx.IsMatch(chatMessage.Message))
                    client.SendMessage(chatMessage.Channel, _keyWordResponse[word]);
            }
        }

        public string[] GetCommands()
        {
            return Array.Empty<string>();
        }
    }
}
