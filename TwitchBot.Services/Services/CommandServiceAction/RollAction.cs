using System.Text.RegularExpressions;
using TwitchBot.Services.Interfaces;
using TwitchLib.Client.Interfaces;
using TwitchLib.Client.Models;

namespace TwitchBot.Services.Services.CommandServiceAction
{
    public class RollAction : ICommandServiceAction
    {
        private static readonly Random RandomDice = new ();
        public bool IsConcern(string message)
        {
            return message.StartsWith("!roll", StringComparison.OrdinalIgnoreCase);
        }

        private readonly Regex _diceRgx = new Regex("([0-9]+)d([0-9]+)", RegexOptions.IgnoreCase | RegexOptions.Compiled);
        public void RunAction(ITwitchClient client, ChatMessage chatMessage)
        {

            client.SendMessage(chatMessage.Channel, ParseMessage(chatMessage.Message));
        }

        public string[] GetCommands()
        {
            return new[] { "!roll", "!roll #d#" };
        }

        public string ParseMessage(string message)
        {
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
                    var diceNumber = Convert.ToInt16(matchGroup[1].Value);
                    var diceValue = Convert.ToInt16(matchGroup[2].Value);
                    var max = diceNumber * diceValue;

                    var rdnResult = RandomDice.Next(diceNumber, max);
                    result = $"Roll {diceNumber}d{diceValue} :";
                    if (rdnResult == diceNumber)
                    {
                        result += " ECHEC CRITIQUE ! Ahah !";
                    }
                    else if (rdnResult == max)
                    {
                        result += " SUCCES CRITIQUE ! Ouah bg !";
                    }
                    else
                    {
                        result += $" {rdnResult}";
                    }
                }
            }
            return result;
        }
    }
}
