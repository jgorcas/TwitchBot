using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using TwitchBot.Services.Interfaces;
using TwitchBot.Services.Services;

namespace TwitchBot.PcV2
{
    internal static class Program
    {
        public static IServiceProvider ServiceProvider { get; private set; } = null!;

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
                    services.AddSingleton<ITwitchClientService, TwitchClientService>();
                    services.AddSingleton<ICommandService, CommandService>();
                    services.AddSingleton<IUserService, UserService>();
                });
        }
        }
    }