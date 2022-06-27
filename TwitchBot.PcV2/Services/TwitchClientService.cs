using Microsoft.Extensions.Configuration;
using Serilog;
using TwitchBot.PcV2.Interfaces;
using TwitchLib.Client;
using TwitchLib.Client.Events;
using TwitchLib.Client.Models;

namespace TwitchBot.PcV2.Services
{
    public sealed class TwitchClientService : ITwitchClientService
    {
     
        private readonly ILogger _logger;
        private readonly IConfiguration _config;
        private  TwitchClient _client = new();

        public TwitchClientService(ILogger logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
            InitializeTwitchClient();
        }

        /// <summary>
        /// Initialize Twitch client information
        /// Subscribe events 
        /// </summary>
        private void InitializeTwitchClient()
        {
            //Using Environment variable to avoid to share Token
            //Visual studio must be restart after environment variable changes
            string[] infoBot = Environment.GetEnvironmentVariable("Twitch_bot")!.Split(';');
            var credentials = new ConnectionCredentials(infoBot[0], infoBot[1]);
            _client.Initialize(credentials, _config["TwitchChannel"]);
            _client.OnLog += ClientOnOnLog;
            _client.OnChatColorChanged += _client_OnChatColorChanged;
        }

        private void _client_OnChatColorChanged(object? sender, OnChatColorChangedArgs e)
        {
            _logger.Debug($"{e.DateTime} - {e.Data}");

        }

        private void ClientOnOnLog(object? sender, OnLogArgs e)
        {
            _logger.Debug($"{e.DateTime} - {e.Data}");
        }

        public void Connect()
        {
            _client.Connect();
            _logger.Debug("Connect twitch bot");
        }

        public void Disconnect()
        {
            if(_client.IsConnected)
                _client.Disconnect();
        }

        public TwitchClient GetTwitchClient()
        {
            if (!_client.IsConnected)
            {
                _client = new TwitchClient();
                InitializeTwitchClient();
            }
            return _client;
        }

        public void SendMessage(string channel, string message)
        {
            var currentChannel = _client.JoinedChannels.FirstOrDefault(c => c.Channel.Equals(channel, StringComparison.OrdinalIgnoreCase));
            _client.SendMessage(currentChannel, message);
        }
    }
}