using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NotionApi;
using NotionApi.Rest;

namespace NotionVisualizer
{
    internal class Program
    {
        private static async Task<int> Main(string[] args)
        {
            var host = CreateHostBuilder().Build();


            var notionClient = host.Services.GetService<INotionClient>() ?? throw new NullReferenceException("Notion client service not available.");

            var searchRequest = notionClient.CreateRequest<Search>();
            searchRequest.Query = "test";

            var result = await searchRequest.Execute();

            return 0;
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