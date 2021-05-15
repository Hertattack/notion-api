using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NotionApi;

namespace NotionVisualizer
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var host = CreateHostBuilder().Build();
            
            
            
            var notionClient = host.Services.GetService<INotionClient>();
            
            
        }

        private static IHostBuilder CreateHostBuilder()
        {
            return Host.CreateDefaultBuilder()
                .UseConsoleLifetime()
                .ConfigureLogging(loggingBuilder =>
                {
                    loggingBuilder.AddConsole();
                    loggingBuilder.SetMinimumLevel(LogLevel.Debug);
                })
                .ConfigureServices(ServiceConfigurator.Configure)
                .ConfigureServices(collection => { collection.AddSingleton<ITokenProvider, TokenProvider>(); });
        }
    }
}