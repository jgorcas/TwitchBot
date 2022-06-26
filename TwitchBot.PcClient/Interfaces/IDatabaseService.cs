using TwitchBot.PcClient.Models;

namespace TwitchBot.PcClient.Interfaces;

public interface IDatabaseService
{
    bool AddNewUser(User user);
    bool UpdateUser(User user);
    IEnumerable<User> GetAllUsers();
    User? GetUserByUsername(string username);
    User? GetUserByTwitchIdOrUserName(long twitchId,string username);
    IEnumerable<UserMessage> GetUserMessages(int idUser);
    bool AddUserMessage(int userId, string message);
}