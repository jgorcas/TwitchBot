using System.Text.RegularExpressions;
using TwitchBot.Services.Interfaces;
using TwitchLib.Client.Interfaces;
using TwitchLib.Client.Models;

namespace TwitchBot.Services.Services.CommandServiceAction
{
    public sealed class HelloMessageAction : ICommandServiceAction
    {
        private readonly Regex _regex = new (@"\b(bonjour|hello|hi|salut|coucou|yo)\b", RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Singleline);
        public bool IsConcern(string message)
        {
            return _regex.IsMatch(message);
        }

        public void RunAction(ITwitchClient client,ChatMessage chatMessage)
        {
            client.SendMessage(chatMessage.Channel,$"Bonjour {chatMessage.Username} ! Comment vas tu ?");
        }

        public string[] GetCommands()
        {
            return Array.Empty<string>();
        }
    }
}