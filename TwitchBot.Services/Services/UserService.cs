using Serilog;
using TwitchBot.Services.Interfaces;
using TwitchLib.Client.Interfaces;

namespace TwitchBot.Services.Services
{
    public class UserService : IUserService
    {
        private readonly ILogger _logger;
        private readonly List<string> _currentUsers;
        public event EventHandler<EventArgs> UserChanged;

        public UserService(ITwitchClientService twitchClientService, ILogger logger)
        {
            _logger = logger;
            ITwitchClient client = twitchClientService.GetTwitchClient();
            client.OnUserJoined += Client_OnUserJoined;
            client.OnUserLeft += Client_OnUserLeft;
            _currentUsers = new List<string>();
        }

        //private void Client_OnUserStateChanged(object? sender, TwitchLib.Client.Events.OnUserStateChangedArgs e)
        //{
        //    _logger.Debug($"Client_OnUserStateChanged - {e.UserState}");
        //}

        private void Client_OnUserLeft(object? sender, TwitchLib.Client.Events.OnUserLeftArgs e)
        {
            _logger.Debug($"Client_OnUserLeft - {e.Username} - {e.Channel}");
            _currentUsers.Remove(e.Username);
            OnUserChanged();
        }

        private void Client_OnUserJoined(object? sender, TwitchLib.Client.Events.OnUserJoinedArgs e)
        {
            _logger.Debug($"Client_OnUserJoined - {e.Username} - {e.Channel}");
            _currentUsers.Add(e.Username);
            OnUserChanged();
        }

        public string[] GetConnectedUsers()
        {
            return _currentUsers.ToArray();
        }

        protected virtual void OnUserChanged()
        {
            UserChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
