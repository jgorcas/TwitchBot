
using TwitchLib.Client;
using TwitchLib.Client.Models;

namespace TwitchBot.PcV2.Interfaces
{
    public interface ITwitchClientService
    {
        void Connect();
        void Disconnect();
        TwitchClient GetTwitchClient();
        void SendMessage(string channel, string message);
    }
}