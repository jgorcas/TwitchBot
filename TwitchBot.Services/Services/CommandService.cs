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
            new TwitchTextCommandsAction()
        };
    }

    private void OnOnMessageReceived(object? sender, OnMessageReceivedArgs e)
    {
        var message = e.ChatMessage.Message;
        _logger.Debug($"CommandFactory - OnOnMessageReceived : {message}");
        foreach (var messageAction in _messageActions.Where(m => m.IsConcern(message)))
        {
            messageAction.RunAction(_client, e.ChatMessage);
        }
    }
}