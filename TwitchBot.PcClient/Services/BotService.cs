using Microsoft.Extensions.Configuration;
using Serilog;
using TwitchBot.PcClient.Interfaces;
using TwitchBot.PcClient.Models;
using TwitchLib.Client;
using TwitchLib.Client.Events;
using TwitchLib.Client.Models;

namespace TwitchBot.PcClient.Services
{
    public sealed class BotService : IBotService
    {
        private readonly IUserService _userService;
        private readonly ILogger _logger;
        private readonly IConfiguration _config;
        private readonly TwitchClient _client = new();
        private readonly List<string> _logs = new();
        
        public IEnumerable<User> ConnectedUsers { get; private set; }

        public BotService(IUserService userService, ILogger logger, IConfiguration config)
        {
            _userService = userService;
            _userService.UserCacheChanged += UserService_UserCacheChanged;
            _logger = logger;
            _config = config;
            ConnectedUsers = Array.Empty<User>();
            InitializeTwitchClient();
        }

        private void UserService_UserCacheChanged(object? sender, EventArgs e)
        {
            ConnectedUsers = _userService.GetAllConnectedUsers();
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
            _client.OnUserJoined += Client_OnUserJoined;
            _client.OnUserLeft += Client_OnUserLeft;
            _client.OnExistingUsersDetected += Client_OnExistingUsersDetected;
            _client.OnMessageReceived += Client_OnMessageReceived;
        }

        #region Events
        private void Client_OnMessageReceived(object? sender, OnMessageReceivedArgs e)
        {
            _logs.Add($"{e.ChatMessage.UserId} - {e.ChatMessage.Username} -  {e.ChatMessage.Message}");
             _userService.UserSendMessage(e.ChatMessage);
        }

        private void Client_OnExistingUsersDetected(object? sender, OnExistingUsersDetectedArgs e)
        {
            e.Users.ForEach(u => _logs.Add($"User detected : {u}"));
        }

        private void Client_OnUserLeft(object? sender, OnUserLeftArgs e)
        {
            _userService.UserLeft(e.Username);
        }

        private void Client_OnUserJoined(object? sender, OnUserJoinedArgs e)
        {
            _userService.UserJoin(e.Username);
        }

        private void ClientOnOnLog(object? sender, OnLogArgs e)
        {
            _logs.Add(e.Data);
            _logger.Information(e.Data);
        }

        #endregion
        #region Methods
        public void Connect()
        {
            _logger.Information("BotService - Connecting");
            _client.Connect();
        }
        public void Disconnect()
        {
            _logger.Information("BotService - Disconnecting");
            if (_client.IsConnected)
                _client.Disconnect();
        }

        public IEnumerable<string> GetLogs()
        {
            return _logs;
        }

        public IEnumerable<User> GetUsers()
        {
            return _userService.GetAllUsers();
        }

        public void ClearLogs()
        {
            _logs.Clear();
        }
        #endregion

    }
}