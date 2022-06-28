using System.Text.RegularExpressions;
using TwitchBot.Services.Interfaces;
using TwitchLib.Client.Interfaces;
using TwitchLib.Client.Models;

namespace TwitchBot.Services.Services.CommandServiceAction
{
    public sealed class HelloMessageAction : ICommandServiceAction
    {
        private readonly Regex _regex = new (@"\b(bonjour|hello|hi|salut)\b", RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Singleline);
        //private readonly string[] _helloWords = { "bonjour","hello","hi","salut" };
        public bool IsConcern(string message)
        {
            //return message.Split(' ').Count(w => _helloWords.Contains(w.Trim().ToLower())) > 0;
            return _regex.IsMatch(message);
        }

        public void RunAction(ITwitchClient client,ChatMessage chatMessage)
        {
            client.SendMessage(chatMessage.Channel,$"Bonjour {chatMessage.Username} ! Comment vas tu ?");
        }
    }
}