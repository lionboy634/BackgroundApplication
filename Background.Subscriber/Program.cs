
using Background.Subscriber;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;

public class Program
{
    public static void Main(string[] args)
    {
        CreateWebHost(args).Build().Run();
    }

    public static IHostBuilder CreateWebHost(string[] args)
    {
        return Host.CreateDefaultBuilder(args)
            .ConfigureServices((builderContext, services) =>
            {
                services.AddHostedService<Subscriber>();
                services.AddSingleton<IConnectionMultiplexer>(c =>
                {
                    var connection = "localhost";
                    return ConnectionMultiplexer.Connect(connection);
                });
            })
            .ConfigureAppConfiguration((builderContext, config) =>
            {
                config.AddJsonFile("appsettings.json", false, true);
                config.AddJsonFile("appsettings.development.json", false, true);
            })
            .ConfigureLogging((builderContext, logging) =>
            {
                logging.AddDebug();
                logging.AddConsole();
            });

    }
}