using TwitchLib.Client;
using TwitchLib.Client.Events;
using TwitchLib.Client.Models;

namespace TwitchBot.PoC;

internal class Bot
{
    private ConnectionCredentials _credentials;
    private TwitchClient _client = new TwitchClient();

    private List<string> _logs;

    public Bot()
    {
        string[] infoBot = Environment.GetEnvironmentVariable("Twitch_bot")!.Split(';');
        _credentials =  new ConnectionCredentials(infoBot[0], infoBot[1]);
        _client.Initialize(_credentials,"elflamby");
        _client.OnLog += ClientOnOnLog;
    }

    private void ClientOnOnLog(object? sender, OnLogArgs e)
    {
        Console.WriteLine(e.Data);
    }

    public void Connect()
    {
        _client.Connect();
        
    }

    public void Disconnect()
    {
        _client.Disconnect();
    }
}