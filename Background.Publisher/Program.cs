
using Background.Publisher;
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
             .ConfigureAppConfiguration((builderContext, config) =>
             {
                 config.AddJsonFile("appsettings.json", false, true);
                 config.AddJsonFile("appsettings.development.json", false, true);
             })
             .ConfigureLogging((builderContext, logging) =>
             {
                 logging.AddConsole();
                 logging.AddDebug();

             })
            .ConfigureServices((builderContext, services) =>
            {
                services.AddHostedService<Publisher>();
                services.RegisterPublisherServices();
                services.AddSingleton<IConnectionMultiplexer>(c =>
                {
                    var connection = "localhost";
                    return ConnectionMultiplexer.Connect(connection);
                });
            });
           
    }
}