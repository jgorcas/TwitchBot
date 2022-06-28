using TwitchLib.Client.Interfaces;
using TwitchLib.Client.Models;

namespace TwitchBot.Services.Interfaces
{
    public interface ICommandServiceAction
    {
        bool IsConcern(string message);
        void RunAction(ITwitchClient client, ChatMessage chatMessage);

    }
}
