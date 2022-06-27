using TwitchLib.Client;

namespace TwitchBot.Services.Interfaces
{
    public interface ITwitchClientService
    {
        void Connect();
        void Disconnect();
        TwitchClient GetTwitchClient();
        void SendMessage(string channel, string message);
    }
}