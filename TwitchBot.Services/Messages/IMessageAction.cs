using TwitchLib.Client;
using TwitchLib.Client.Models;

namespace TwitchBot.Services.Messages
{
    public interface IMessageAction
    {
        bool IsConcern(string message);
        void RunAction(TwitchClient client, ChatMessage chatMessage);

    }
}
