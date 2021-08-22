﻿using System;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Logging;
using NotionApi;
using NotionApi.Rest.Search;
using NotionVisualizer.Generator;
using Util;

namespace NotionVisualizer
{
    public class Visualizer
    {
        private readonly ILogger<Visualizer> _logger;
        private readonly INotionClient _notionClient;
        private readonly IGenerator _generator;

        public Visualizer(
            ILogger<Visualizer> logger,
            INotionClient notionClient,
            CytoscapeGenerator generator)
        {
            _logger = logger;
            _notionClient = notionClient;
            _generator = generator;
        }

        public int Execute(string outputFolder, Option<string> requestResourcePath, bool clean)
        {
            _logger.LogInformation("Starting visualization generation...");

            var searchRequest = new SearchRequest();

            var response = requestResourcePath.HasValue
                ? _notionClient.ReadFromDisk(searchRequest, requestResourcePath.Value).Result
                : _notionClient.ExecuteRequest(searchRequest).Result;

            if (!response.HasValue)
            {
                Console.WriteLine("No response received.");
                return 0;
            }

            var result = response.Value;
            var cache = _notionClient.CreateCache();
            cache.Update(result.Results);

            if (cache.CacheMisses.Any())
            {
                Console.WriteLine("Cache misses:");
                foreach (var cacheMiss in cache.CacheMisses)
                    Console.WriteLine(cacheMiss.Description);
            }

            if (clean)
            {
                _logger.LogInformation("Cleaning output folder: {outputFolder}", outputFolder);

                foreach (var directory in Directory.GetDirectories(outputFolder))
                    Directory.Delete(directory, true);

                foreach (var file in Directory.GetFiles(outputFolder))
                    Directory.Delete(file);
            }

            _generator.Generate(outputFolder, cache, result.Results);

            _logger.LogInformation("Visualization generation finished to output path: {outputFolder}", outputFolder);
            return 0;
        }
    }
}