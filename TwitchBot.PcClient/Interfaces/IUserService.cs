using TwitchBot.PcClient.Models;
using TwitchLib.Client.Models;

namespace TwitchBot.PcClient.Interfaces;

public interface IUserService
{
    event EventHandler UserCacheChanged;
    void UserJoin(string username);
    void UserSendMessage(ChatMessage chatMessage);
    //User UnknownUserSendMessage(ChatMessage chatMessage);
    IEnumerable<User> GetAllUsers();
    ICollection<User> GetAllConnectedUsers(DateTime? minDateTime);
    void UserLeft(string username);
}