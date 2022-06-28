using Serilog;
using TwitchBot.Services.Interfaces;
using TwitchBot.Services.Services.CommandServiceAction;
using TwitchLib.Client.Events;
using TwitchLib.Client.Interfaces;

namespace TwitchBot.Services.Services;

public class CommandService : ICommandService
{
    private readonly ILogger _logger;
    private readonly ITwitchClient _client;
    private readonly ICommandServiceAction[] _messageActions;

    public CommandService(ILogger logger, ITwitchClientService twitchClientService)
    {
        _logger = logger;
        _client = twitchClientService.GetTwitchClient();
        _client.OnMessageReceived += OnOnMessageReceived;
        _messageActions = new ICommandServiceAction[]
        {
            new HelloMessageAction(),
            new TextCommandsAction(),
            new RollAction()
        };
    }

    private void OnOnMessageReceived(object? sender, OnMessageReceivedArgs e)
    {
        var message = e.ChatMessage.Message;
        _logger.Debug($"CommandFactory - OnOnMessageReceived : {message}");
        if (message.Equals("!commands"))
        {
            var commands = _messageActions.SelectMany(m => m.GetCommands()).ToArray();
            Array.Sort(commands);
            _client.SendMessage(e.ChatMessage.Channel, $"Commandes : {string.Join(" | ",commands)}");
            return;
        }
        foreach (var messageAction in _messageActions.Where(m => m.IsConcern(message)))
        {
            messageAction.RunAction(_client, e.ChatMessage);
        }
    }
}