namespace TwitchBot.Services.Interfaces;

public interface IUserService
{
    event EventHandler<EventArgs> UserChanged;
    string[] GetConnectedUsers();
}