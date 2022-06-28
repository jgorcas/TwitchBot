using Microsoft.Extensions.Configuration;
using Serilog;
using TwitchBot.Services.Interfaces;
using TwitchLib.Client;
using TwitchLib.Client.Events;
using TwitchLib.Client.Models;

namespace TwitchBot.Services.Services
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
            if (!_client.IsConnected) return;
            _client.Disconnect();
            _logger.Debug("Disconnect twitch bot");
        }

        public TwitchClient GetTwitchClient()
        {
            return _client;
        }
    }
}