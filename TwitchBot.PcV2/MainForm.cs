using System.ComponentModel;
using TwitchBot.PcV2.Interfaces;

namespace TwitchBot.PcV2
{
    public partial class MainForm : Form
    {
        private readonly ITwitchClientService _twitchClientService;
        private readonly ICommandService _commandService;

        public MainForm(ITwitchClientService twitchClientService, ICommandService commandService)
        {
            _twitchClientService = twitchClientService;
            _commandService = commandService;
            InitializeComponent();

        }

        private void bConnect_Click(object sender, EventArgs e)
        {
            _twitchClientService.Connect();
            _twitchClientService.GetTwitchClient().OnLog += MainForm_OnLog;
        }

        private void MainForm_OnLog(object? sender, TwitchLib.Client.Events.OnLogArgs e)
        {
            //if (lbLogs.InvokeRequired)
            //{
            //    Action safeWrite = delegate { lbLogs.Items.Add(e.Data); };
            //    lbLogs.Invoke(safeWrite);
            //}
            //else
            //    lbLogs.Items.Add(e.Data);
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