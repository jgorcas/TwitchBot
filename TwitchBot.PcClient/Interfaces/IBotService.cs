using TwitchBot.PcClient.Models;

namespace TwitchBot.PcClient.Interfaces;

public interface IBotService
{
    void Connect();
    void Disconnect();
    IEnumerable<string> GetLogs();
    IEnumerable<User> GetUsers();
    IEnumerable<User> ConnectedUsers { get; }
    void ClearLogs();
}