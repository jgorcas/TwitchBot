
using System.Collections.Concurrent;
using Serilog;
using TwitchBot.PcClient.Interfaces;
using TwitchBot.PcClient.Models;
using TwitchLib.Client.Models;

namespace TwitchBot.PcClient.Services
{
    public sealed class UserService : IUserService
    {
        private readonly IDatabaseService _dbService;
        private readonly ILogger _logger;
        private readonly ConcurrentDictionary<string, User> _userCache = new();
        public event EventHandler UserCacheChanged;
        public UserService(IDatabaseService dbService, ILogger logger)
        {
            _dbService = dbService;
            _logger = logger;
            _userCache = new ConcurrentDictionary<string, User>(_dbService.GetAllUsers().ToDictionary(u => u.UserName, v => v), StringComparer.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Create user if it doesn't exists 
        /// Update LastConnectionDate
        /// </summary>
        /// <param name="user"></param>
        private void AddOrUpdateUser(User user)
        {
            if (!_userCache.ContainsKey(user.UserName))
            {
                _dbService.AddNewUser(user);

                if (_userCache.TryAdd(user.UserName, user))
                {
                    _logger.Error($"UserService - UserJoin - Add {user.UserName} failed");
                    return;
                }
                _logger.Debug($"Add new joining user : {user.UserName}");
            }
            else
            {
                if (user.IdTwitch != null && _userCache[user.UserName].IdTwitch != null)
                {
                    _userCache[user.UserName].IdTwitch = user.IdTwitch;
                }
                _userCache[user.UserName].LastConnectionDate = user.LastConnectionDate;
                _dbService.UpdateUser(_userCache[user.UserName]);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        public void UserJoin(string username)
        {
            var user = new User(username);
            AddOrUpdateUser(user);
            OnUserCacheChanged();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="chatMessage"></param>
        public void UserSendMessage(ChatMessage chatMessage)
        {
            User user = new User(chatMessage.Username)
            {
                IdTwitch = Convert.ToInt64(chatMessage.UserId),
                LastMessageDate = DateTime.Now
            };
            AddOrUpdateUser(user);

            _dbService.AddUserMessage(_userCache[chatMessage.Username].Id, chatMessage.Message);
            _userCache[chatMessage.Username].Messages.Add(new UserMessage
            {
                Text = chatMessage.Message,
                CreatedDate = DateTime.Now,
                
            });
            OnUserCacheChanged();
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _dbService.GetAllUsers();
        }

        public ICollection<User> GetAllConnectedUsers(DateTime? minDateTime)
        {
            if(!minDateTime.HasValue)
                return _userCache.Values;
            return _userCache.Values.Where(u => u.LastConnectionDate > minDateTime.Value).ToArray();
        }

        public void UserLeft(string username)
        {
            _userCache.TryRemove(username, out _);
            OnUserCacheChanged();
        }
        private void OnUserCacheChanged()
        {
            UserCacheChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
