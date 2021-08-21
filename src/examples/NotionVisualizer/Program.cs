using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NotionApi;
using NotionApi.Rest;
using NotionApi.Rest.Objects;
using NotionApi.Rest.Search;
using NotionVisualizer.SigmaJs;
using NotionVisualizer.Util;
using RestUtil;
using Util;

namespace NotionVisualizer
{
    internal static class Program
    {
#if DEBUG
        private static bool IsDebug => true;
#else
        private static bool IsDebug => false;
#endif

        private static int Main(string[] args)
        {
            var serviceProvider = CreateServiceProvider(args);

            var commandLineOptions = ParseCommandLine(args).ToList();

            return Go(serviceProvider, commandLineOptions);
        }

        private static IEnumerable<CommandLineOptionValue> ParseCommandLine(IEnumerable<string> args)
        {
            var parser = new CommandLineParser(
                new CommandLineOption
                {
                    Name = "source",
                    Required = false,
                    Description = "The source json files to use instead of sending requests to Notion.",
                    HasValue = true
                },
                new CommandLineOption
                {
                    Name = "output",
                    Required = true,
                    Description = "The output folder to use.",
                    HasValue = true
                },
                new CommandLineOption
                {
                    Name = "clean",
                    Required = false,
                    Description = "Clean the deployment folder before deploying.",
                    HasValue = false
                });

            try
            {
                return parser.Parse(args);
            }
            catch
            {
                Console.WriteLine("The following command line options are available:");
                Console.WriteLine(parser.GetDescription());
                throw;
            }
        }

        private static int Go(IServiceProvider serviceProvider, IReadOnlyList<CommandLineOptionValue> commandLineOptionValues)
        {
            var options = serviceProvider.GetService<IOptions<NotionVisualizerOptions>>()?.Value;
            if (options is null)
                throw new ArgumentException("Please specify the Notion Visualizer options in the app settings.");

            var source = commandLineOptionValues.FirstOrDefault(c => c.Option.Name == "source")?.Value;
            var outputFolder = commandLineOptionValues.First(c => c.Option.Name == "output").Value;
            var clean = commandLineOptionValues.Any(c => c.Option.Name == "clean");

            var notionClient = serviceProvider.GetService<INotionClient>() ?? throw new NullReferenceException("Notion client service not available.");

            var searchRequest = new SearchRequest();

            var response = source is null
                ? notionClient.ExecuteRequest(searchRequest).Result
                : notionClient.ReadFromDisk(searchRequest, source).Result;

            if (!response.HasValue)
            {
                Console.WriteLine("No response received.");
                return 0;
            }

            var result = response.Value;
            var cache = notionClient.CreateCache();
            cache.Update(result.Results);

            if (cache.CacheMisses.Any())
            {
                Console.WriteLine("Cache misses:");
                foreach (var cacheMiss in cache.CacheMisses)
                    Console.WriteLine(cacheMiss.Description);
            }

            var deployer = serviceProvider.GetService<ISigmaJsDeployer>();
            deployer.Deploy(outputFolder, options.SigmaJsPackage);

            return 0;
        }

        private static IServiceProvider CreateServiceProvider(string[] args)
        {
            var configurationBuilder = new ConfigurationBuilder();

            configurationBuilder.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            if (IsDebug)
                configurationBuilder.AddJsonFile("appsettings.Debug.json", optional: true, reloadOnChange: true);

            IConfiguration configuration = configurationBuilder
                .AddEnvironmentVariables()
                .AddCommandLine(args)
                .Build();

            return CreateServiceContainer(configuration);
        }

        private static IServiceProvider CreateServiceContainer(IConfiguration configuration)
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.Configure<NotionVisualizerOptions>(o => configuration.GetSection(nameof(NotionVisualizer)).Bind(o));
            serviceCollection.Configure<NotionClientOptions>(o => configuration.GetSection(nameof(NotionClient)).Bind(o));
            serviceCollection.Configure<RestClientOptions>(o => configuration.GetSection(nameof(RestClient)).Bind(o));

            serviceCollection.AddLogging(loggingBuilder =>
            {
                loggingBuilder
                    .AddConsole()
                    .AddConfiguration(configuration.GetSection("Logging"));
            });

            serviceCollection.AddTransient<IRestClient, RestClient>();

            ServiceConfigurator.Configure(serviceCollection);

            serviceCollection.AddTransient<INotionClient, NotionClient>();

            serviceCollection.AddTransient<ISigmaJsDeployer, SigmaJsDeployer>();

            return serviceCollection.BuildServiceProvider();
        }
    }
}