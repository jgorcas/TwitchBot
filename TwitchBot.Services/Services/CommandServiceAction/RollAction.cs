using System.Text.RegularExpressions;
using TwitchBot.Services.Interfaces;
using TwitchLib.Client.Interfaces;
using TwitchLib.Client.Models;

namespace TwitchBot.Services.Services.CommandServiceAction
{
    public class RollAction : ICommandServiceAction
    {
        private static readonly Random RandomDice = new();
        public bool IsConcern(string message)
        {
            return message.StartsWith("!roll", StringComparison.OrdinalIgnoreCase);
        }

        private readonly Regex _diceRgx = new Regex("([0-9]+)d([0-9]+)", RegexOptions.IgnoreCase | RegexOptions.Compiled);
        public void RunAction(ITwitchClient client, ChatMessage chatMessage)
        {

            client.SendMessage(chatMessage.Channel, ParseMessage(chatMessage));
        }

        public string[] GetCommands()
        {
            return new[] { "!roll", "!roll #d#" };
        }

        public string ParseMessage(ChatMessage chatMessage)
        {
            var message = chatMessage.Message;
            string result = "Houston on a un problème ! ";
            var rollInfo = message.ToLower().Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            if (rollInfo.Length == 1)
            {
                result = $"Roll 1d100 : {RandomDice.Next(1, 100)}";
            }
            else
            {
                var matches = _diceRgx.Matches(rollInfo[1]);
                if (matches.Count > 0)
                {
                    var matchGroup = matches[0].Groups;
                    if (!short.TryParse(matchGroup[1].Value, out var diceNumber) || !short.TryParse(matchGroup[2].Value, out var diceValue))
                    {
                        return result;
                    }
                    if (diceNumber == 0 || diceValue == 0 || diceNumber > 100)
                    {
                        return result;
                    }

                    int[] rolls = new int[diceNumber];
                    for (int i = 0; i < diceNumber; i++)
                    {
                        rolls[i] = RandomDice.Next(1, diceValue + 1);
                    }

                    var rdnResult = rolls.Sum();

                    result = $"Roll {diceNumber}d{diceValue} :";

                    result += $" {rdnResult} ({string.Join('+', rolls.Select(r => r==1?"1!":$"{r}"))})";
                    if (result.Length > 500)
                        result = "Houston on a un problème ! ";
                }
            }
            return $"@{chatMessage.Username} - {result}";
        }
    }
}
