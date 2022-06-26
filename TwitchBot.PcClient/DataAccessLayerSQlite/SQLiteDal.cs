using Microsoft.Extensions.Configuration;
using Serilog;
using TwitchBot.PcClient.Interfaces;

namespace TwitchBot.PcClient.DataAccessLayerSQlite
{
    public partial class SQLiteDal : IDatabaseService
    {
        private readonly IConfiguration _config;
        private readonly ILogger _logger;
        private readonly string _connectionString;
        public SQLiteDal(IConfiguration config, ILogger logger)
        {
            _config = config;
            _logger = logger;
            _connectionString = _config.GetConnectionString("SQliteDB");
        }
    }
}
