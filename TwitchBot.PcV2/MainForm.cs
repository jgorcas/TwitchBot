using System.ComponentModel;
using TwitchBot.Services.Interfaces;

namespace TwitchBot.PcV2
{
    public partial class MainForm : Form
    {
        private readonly ITwitchClientService _twitchClientService;

        private readonly ICommandService _commandFactory;
        //private readonly ICommandService _commandService;

        public MainForm(ITwitchClientService twitchClientService, ICommandService commandFactory)
        {
            _twitchClientService = twitchClientService;
            _commandFactory = commandFactory;
            //_commandService = commandService;
            InitializeComponent();

        }

        private void bConnect_Click(object sender, EventArgs e)
        {
            _twitchClientService.Connect();
            _twitchClientService.GetTwitchClient().OnLog += MainForm_OnLog;
        }

        private void MainForm_OnLog(object? sender, TwitchLib.Client.Events.OnLogArgs e)
        {
            this.Invoke(() => { lbLogs.Items.Add(e.Data); });
        }

        private void bDisconnect_Click(object sender, EventArgs e)
        {
            _twitchClientService.Disconnect();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            _twitchClientService.Disconnect();
        }

        private void bActivateCommands_Click(object sender, EventArgs e)
        {

        }
    }
}