using TwitchBot.PcClient.Interfaces;

namespace TwitchBot.PcClient.Services;

public class CommandService : ICommandService
{
    public string ReadMessage(string chatMessageMessage)
    {
        var resultMessage = string.Empty;

        if (!chatMessageMessage.StartsWith('!') && chatMessageMessage.Length < 2) return resultMessage;

        var message = chatMessageMessage[1..].Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        var command = message[0].ToLower();
        switch (command)
        {
            case "github":
                resultMessage = @"Super lien github : http:\\www.github.com\jgorcas";
                break;
            case "roll":
                Random rdn = new Random(DateTime.Now.Millisecond);
                var min = 1;
                var max = 100;
                if (message.Length == 2 && int.TryParse(message[1], out var newMax) && newMax > 1)
                {
                    max = newMax;
                }
                if (message.Length == 3)
                {
                    var boolVar1 = int.TryParse(message[1], out var var1);
                    var boolVar2 = int.TryParse(message[2], out var var2);
                    if (boolVar1 && boolVar2)
                    {
                        if (var1 >= var2)
                        {
                            max = var1;
                            min = var2;
                        }
                        else
                        {
                            max = var2;
                            min = var1;
                        }
                    }
                    else if (boolVar1 && var1 >= min && var1 < max)
                    {
                        min = var1;
                    }
                    else if (boolVar2 && var2 > min)
                    {
                        max = var2;
                    }
                }
                resultMessage = $@"Roll {min}-{max} = {rdn.Next(min, max)}";
                break;
        }

        return resultMessage;
    }
}