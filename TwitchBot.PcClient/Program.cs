using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using TwitchBot.PcClient.Forms;
using TwitchBot.PcClient.Services;

namespace TwitchBot.PcClient
{
    internal static class Program
    {

        public static IServiceProvider ServiceProvider { get; private set; }

        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            var host = CreateHostBuilder().Build();
            ServiceProvider = host.Services;
            Log.Information("Application running");
            Application.Run(ServiceProvider.GetRequiredService<MainForm>());
        }

        private static IHostBuilder CreateHostBuilder()
        {
            return Host.CreateDefaultBuilder()
                .UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration))
                .ConfigureServices((context, services) =>
                {
                    services.AddTransient<MainForm>();
                    services.AddSingleton<IBotService, BotService>();
                });
        }
    }
}