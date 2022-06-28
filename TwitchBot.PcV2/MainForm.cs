using System.ComponentModel;
using TwitchBot.Services.Interfaces;
using TwitchLib.Client.Events;

namespace TwitchBot.PcV2
{
    public partial class MainForm : Form
    {
        private readonly ITwitchClientService _twitchClientService;
        private readonly ICommandService _commandFactory;
        private readonly IUserService _userService;

        public MainForm(ITwitchClientService twitchClientService, ICommandService commandFactory,IUserService userService)
        {
            _twitchClientService = twitchClientService;
            _commandFactory = commandFactory;
            _userService = userService;
            InitializeComponent();

        }

        private void bConnect_Click(object sender, EventArgs e)
        {
            _twitchClientService.Connect();
            _twitchClientService.GetTwitchClient().OnLog += MainForm_OnLog;
            _userService.UserChanged += UserServiceOnUserChanged;
        }

        private void UserServiceOnUserChanged(object? sender, EventArgs e)
        {
            Invoke(() => { lbUsers.Items.Clear(); lbUsers.Items.AddRange(_userService.GetConnectedUsers()); });
        }


        private void MainForm_OnLog(object? sender, OnLogArgs e)
        {
            Invoke(() => { lbLogs.Items.Add(e.Data); });
        }

        private void bDisconnect_Click(object sender, EventArgs e)
        {
            _twitchClientService.Disconnect();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            _twitchClientService.Disconnect();
        }
    }
}