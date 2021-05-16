using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NotionApi;
using NotionApi.Commands;

namespace NotionVisualizer
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var host = CreateHostBuilder().Build();
            
            
            
            var notionClient = host.Services.GetService<INotionClient>();
            notionClient.ApiVersion = Version.V20210513;

            var searchBuilder = notionClient.GetCommandBuilder<Search>();
            
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