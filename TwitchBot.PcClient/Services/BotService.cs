using Serilog;
using TwitchLib.Client;
using TwitchLib.Client.Events;
using TwitchLib.Client.Models;

namespace TwitchBot.PcClient.Services
{
    public interface IBotService
    {
        void Connect();
        void Disconnect();
        IEnumerable<string> GetLogs();
        IEnumerable<string> GetUsers();
        void ClearLogs();
    }
    public sealed class BotService : IBotService
    {
        private readonly ILogger _logger;
        private ConnectionCredentials _credentials;
        private TwitchClient _client = new();
        private List<string> _logs;
        private List<string> _users;
        public BotService(ILogger logger)
        {
            _logger = logger;
            string[] infoBot = Environment.GetEnvironmentVariable("Twitch_bot")!.Split(';');
            _logs = new List<string>();
            _users = new List<string>();
            _credentials = new ConnectionCredentials(infoBot[0], infoBot[1]);
            _client.Initialize(_credentials, "elflamby");
            _client.OnLog += ClientOnOnLog;


            _client.OnUserJoined += ClientOnUserJoined;
            _client.OnUserLeft += ClientOnUserLeft;
        }

        private void ClientOnUserLeft(object? sender, OnUserLeftArgs e)
        {
            _users.Remove(e.Username);
        }

        private void ClientOnUserJoined(object? sender, OnUserJoinedArgs e)
        {
            _users.Add(e.Username);
        }

        private void ClientOnOnLog(object? sender, OnLogArgs e)
        {
            _logs.Add(e.Data);
            _logger.Information(e.Data);
        }

        public void Connect()
        {
            _logger.Information("BotService - Connecting");
            _client.Connect();
        }

        public void Disconnect()
        {
            _logger.Information("BotService - Disconnecting");
            if(_client.IsConnected)
                _client.Disconnect();
        }

        public IEnumerable<string> GetLogs()
        {
            return _logs;
        }

        public IEnumerable<string> GetUsers()
        {
            return _users;
        }

        public void ClearLogs()
        {
            _logs.Clear();
        }
    }
}