using System.ComponentModel;
using TwitchBot.PcClient.Services;
using Serilog;
using Timer = System.Windows.Forms.Timer;

namespace TwitchBot.PcClient.Forms
{
    public partial class MainForm : Form
    {
        private readonly IBotService _botService;
        private readonly ILogger _logger;
        private Timer _refreshTimer;
        private BindingSource _logsBinding;
        private BindingSource _usersBinding;
        public MainForm(IBotService botService,ILogger logger)
        {
            _botService = botService;
            _logger = logger;
            _refreshTimer = new Timer
            {
                Interval = 1000
            };
            _refreshTimer.Tick += RefreshTimerTick;
            _refreshTimer.Start();
            _logsBinding = new BindingSource();
            _usersBinding = new BindingSource();
            InitializeComponent();
            lbLogs.DataSource = _logsBinding;
            lbUsers.DataSource = _usersBinding;
        }

        private void RefreshTimerTick(object? sender, EventArgs e)
        {
            _logsBinding.DataSource = _botService.GetLogs();
            _logsBinding.ResetBindings(true);
            _usersBinding.DataSource = _botService.GetUsers();
            _usersBinding.ResetBindings(true);

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
                _logger.Error(ex,"Connection failed");
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
        #endregion

        private void bClearLogs_Click(object sender, EventArgs e)
        {
            _botService.ClearLogs();
        }

        private void bRefreshLogs_Click(object sender, EventArgs e)
        {
            
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            _refreshTimer.Stop();
            _botService.Disconnect();
            base.OnClosing(e);
        }
    }
}