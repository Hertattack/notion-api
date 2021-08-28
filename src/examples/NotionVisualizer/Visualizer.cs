using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NotionApi;
using NotionApi.Cache;
using NotionApi.Rest.Request.Database;
using NotionApi.Rest.Response.Database;
using NotionApi.Rest.Response.Database.Properties;
using NotionApi.Rest.Response.Objects;
using NotionApi.Rest.Response.Page;
using NotionApi.Rest.Response.Page.Properties.Relation;
using NotionVisualizer.Generator;
using NotionVisualizer.Visualization;

namespace NotionVisualizer
{
    public class Visualizer
    {
        private readonly ILogger<Visualizer> _logger;
        private readonly INotionClient _notionClient;
        private readonly IReadOnlyList<IGenerator> _generators;
        private readonly NotionVisualizerOptions _options;

        private readonly GraphBuilder _graphBuilder;
        private readonly Dictionary<string, EdgeDirection[]> _edgeDirections;

        public Visualizer(
            IOptions<NotionVisualizerOptions> visualizerOptions,
            ILogger<Visualizer> logger,
            INotionClient notionClient,
            IEnumerable<IGenerator> generators)
        {
            _logger = logger;
            _notionClient = notionClient;
            _generators = generators.ToList();
            _options = visualizerOptions.Value;

            _graphBuilder = new GraphBuilder { NodeFilter = NodeFilter };

            if (_options.EdgeDirections.Any())
                _graphBuilder.EdgeFilter = EdgeFilter;

            _edgeDirections = _options.EdgeDirections
                .GroupBy(e => e.SourceContainer)
                .ToDictionary(
                    e => e.Key,
                    v => v.Select(ed => ed).ToArray());
        }

        private static bool NodeFilter(NotionObject node) =>
            node is PageObject;

        private bool EdgeFilter(NotionObject source, string propertyName, OneToManyRelationPropertyValue relationPropertyValue)
        {
            if (source is not PageObject page)
                return false;

            if (!page.Container.HasValue)
                return false;

            if (page.Container.Value is not DatabaseObject databaseContainer)
                return false;

            if (!_edgeDirections.TryGetValue(databaseContainer.Id, out var acceptedTargets))
                return false;

            if (!relationPropertyValue.Configuration.HasValue)
                return false;

            if (relationPropertyValue.Configuration.Value is not RelationPropertyConfiguration relationPropertyConfiguration)
                return false;

            var containerId = relationPropertyConfiguration.Configuration.DatabaseId;
            return containerId != null
                   && acceptedTargets.Any(ed =>
                       ed.TargetContainer == containerId
                       && (ed.PropertyName is null || ed.PropertyName == propertyName));
        }

        public int Execute(string outputFolder, bool clean)
        {
            _logger.LogInformation("Starting visualization generation...");

            var cache = _notionClient.CreateCache();
            var notionObjectsToVisualize = FetchDataFromApi(cache);

            CleanOutputFolder(outputFolder, clean);

            _logger.LogInformation("Building graph for {nrOfObject} objects.", notionObjectsToVisualize.Count);
            var graph = _graphBuilder.Build(cache, notionObjectsToVisualize);
            _logger.LogInformation("Done building graph");

            if (_options.FilterNodesWithNoEdges)
            {
                var referencedIds = graph.Edges.SelectMany(e => new[] { e.SourceId, e.TargetId }).ToHashSet();
                foreach (var nodeWithoutEdges in graph.Nodes.Where(n => !referencedIds.Contains(n.Id)).ToList())
                    graph.Nodes.Remove(nodeWithoutEdges);
            }

            GenerateOutput(outputFolder, graph);

            _logger.LogInformation("Visualization generation finished to output path: {outputFolder}", outputFolder);

            return 0;
        }

        private void GenerateOutput(string outputFolder, Graph graph)
        {
            foreach (var generator in _generators.Where(g => _options.UseGenerators.Contains(g.Name)))
            {
                try
                {
                    var generatorOutputPath = Path.Combine(outputFolder, generator.Name);
                    _logger.LogInformation("Generating output using generator: {generatorName} into: '{outputFolder}'",
                        generator.Name, generatorOutputPath);

                    if (!Directory.Exists(generatorOutputPath))
                        Directory.CreateDirectory(generatorOutputPath);

                    generator.Generate(generatorOutputPath, graph);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error generating output");
                }
            }
        }

        private void CleanOutputFolder(string outputFolder, bool clean)
        {
            if (!clean)
                return;

            _logger.LogInformation("Cleaning output folder: {outputFolder}", outputFolder);

            foreach (var directory in Directory.GetDirectories(outputFolder))
                Directory.Delete(directory, true);

            foreach (var file in Directory.GetFiles(outputFolder))
                File.Delete(file);
        }

        private IList<NotionObject> FetchDataFromApi(INotionCache cache)
        {
            var indexedObjects = new Dictionary<string, NotionObject>();
            foreach (var database in _options.Databases)
            {
                var databaseRequest = new DatabaseDefinitionRequest { DatabaseId = database };
                var response = _notionClient.ExecuteRequest(databaseRequest).Result;

                if (!response.HasValue)
                {
                    _logger.LogWarning("Database: '{databaseId}' was not found.", database);
                    continue;
                }

                var databaseDefinition = response.Value;
                indexedObjects[databaseDefinition.Id] = databaseDefinition;

                var databaseContentsRequest = new SearchDatabaseRequest { DatabaseId = database };
                var databaseContentsResponse = _notionClient.ExecuteRequest(databaseContentsRequest).Result;
                if (!databaseContentsResponse.HasValue)
                {
                    _logger.LogWarning("Database: '{databaseId}' has no entries.", database);
                    continue;
                }

                var databaseContents = databaseContentsResponse.Value;
                foreach (var databaseEntry in databaseContents.Results)
                    indexedObjects[databaseEntry.Id] = databaseEntry;
            }

            var notionObjectsToVisualize = indexedObjects.Values.ToList();
            cache.Update(notionObjectsToVisualize);

            foreach (var cacheMiss in cache.CacheMisses)
                _logger.LogDebug("Cache miss: {description}", cacheMiss.Description);

            return notionObjectsToVisualize;
        }
    }
}