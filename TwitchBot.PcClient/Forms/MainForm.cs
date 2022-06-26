using System.ComponentModel;
using Serilog;
using TwitchBot.PcClient.Models;
using Timer = System.Windows.Forms.Timer;
using TwitchBot.PcClient.Interfaces;

namespace TwitchBot.PcClient.Forms
{
    public partial class MainForm : Form
    {
        private readonly IBotService _botService;
        private readonly ILogger _logger;
        private readonly Timer _refreshTimer;
        public MainForm(IBotService botService, ILogger logger)
        {
            _botService = botService;
            _logger = logger;
            _refreshTimer = new Timer
            {
                Interval = 3000
            };
            _refreshTimer.Tick += RefreshTimerTick;
            _refreshTimer.Start();
            InitializeComponent();
            lbLogs.DataSource = _botService.GetLogs();
            lbUsers.DisplayMember = "UserName";
            lbUsers.ValueMember = "Id";
            //lbUsers.DataSource = _botService.ConnectedUsers;
            lbMessages.DataSource = Array.Empty<UserMessage>();
        }

        private void RefreshTimerTick(object? sender, EventArgs e)
        {
            var logs = _botService.GetLogs().ToArray();
            lbLogs.DataSource = logs;
            lbLogs.SelectedIndex = logs.Length - 1;
            var selectedUser = lbUsers.SelectedValue;
            lbUsers.DataSource = _botService.ConnectedUsers.ToArray();
            if (selectedUser != null)
                lbUsers.SelectedValue = selectedUser;
            LoadUserMessages();
        }


        private void LoadUserMessages()
        {
            if (lbUsers.SelectedItem != null)
            {
                var user = _botService.ConnectedUsers.FirstOrDefault(u => u.Id == (int)lbUsers.SelectedValue);
                if (user != null)
                {
                    lbMessages.DataSource = user.Messages;
                }
            }
        }
        #region Event
        private void bConnect_Click(object sender, EventArgs e)
        {
            try
            {
                _botService.Connect();
                _logger.Information("Connection done");
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Connection failed");
                //todo label info
            }
        }
        private void bDisconnect_Click(object sender, EventArgs e)
        {
            try
            {
                _botService.Disconnect();
                _logger.Information("Disconnection done");

            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Disconnection failed");

                //todo label info
            }
        }
        private void lbUsers_SelectedValueChanged(object sender, EventArgs e)
        {
            LoadUserMessages();
        }

        private void bClearLogs_Click(object sender, EventArgs e)
        {
            _botService.ClearLogs();
        }
        protected override void OnClosing(CancelEventArgs e)
        {
            _refreshTimer.Stop();
            _botService.Disconnect();
            base.OnClosing(e);
        }
        #endregion





    }
}