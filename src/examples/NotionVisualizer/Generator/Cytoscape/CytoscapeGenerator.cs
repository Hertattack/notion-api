using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using NotionApi.Cache;
using NotionApi.Rest.Database;
using NotionApi.Rest.Database.Properties;
using NotionApi.Rest.Objects;
using NotionApi.Rest.Page;
using NotionApi.Rest.Page.Properties.Relation;
using NotionVisualizer.Generator.Cytoscape.Graph;

namespace NotionVisualizer.Generator.Cytoscape
{
    public class CytoscapeGenerator : IGenerator
    {
        private const string _cytoscapeJsFileName = "cytoscape.min.js";
        private const string _indexHtmlFileName = "index.html";
        private const string _dataFileName = "data.js";
        private const string _configurationFileName = "configuration.js";
        private static readonly string _cytoscapeResourcePath = Path.Join("Resources", "cytoscape");

        private readonly ILogger<CytoscapeGenerator> _logger;
        private readonly CytoscapeGeneratorOptions _options;

        public CytoscapeGenerator(
            ILogger<CytoscapeGenerator> logger,
            IOptions<CytoscapeGeneratorOptions> options)
        {
            _logger = logger;
            _options = options.Value;
        }

        public void Generate(string outputPath, INotionCache cache, IList<NotionObject> notionObjects)
        {
            _logger.LogInformation("Generation Cytoscape output to path: {outputPath}", outputPath);

            File.Copy(Path.Join(_cytoscapeResourcePath, _cytoscapeJsFileName), Path.Join(outputPath, _cytoscapeJsFileName));
            File.Copy(Path.Join(_cytoscapeResourcePath, _indexHtmlFileName), Path.Join(outputPath, _indexHtmlFileName));

            var data = BuildData(cache, notionObjects);
            WriteJavaScriptFile(Path.Join(outputPath, _dataFileName), "data", data);

            var configuration = new Configuration
            {
                LayoutAlgorithm = _options.LayoutAlgorithm
            };
            WriteJavaScriptFile(Path.Join(outputPath, _configurationFileName), "configuration", configuration);

            _logger.LogInformation("Generation finished.");
        }

        private void WriteJavaScriptFile(string filePath, string variableName, object data)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append($"var {variableName} = ");
            stringBuilder.Append(
                JsonConvert.SerializeObject(data, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }));

            File.WriteAllText(filePath, stringBuilder.ToString());
        }

        private IEnumerable<object> BuildData(INotionCache notionCache, IList<NotionObject> notionObjects)
        {
            var index = notionObjects.ToDictionary(n => n.Id);

            var nodes = new List<Node>();
            var edges = new List<Edge>();

            Func<PageObject, bool> nodeFilter = _ => true;

            if (!string.IsNullOrEmpty(_options.NodeSource))
                nodeFilter = FilterOnSource;

            Func<DatabaseObject, bool> databaseFilter = _ => true;
            if (!string.IsNullOrEmpty(_options.TagDatabase))
                databaseFilter = FilterTagDatabase;

            foreach (var notionPage in notionObjects.OfType<DatabaseObject>().Where(databaseFilter))
            {
                var node = new Node
                {
                    Id = notionPage.Id,
                    Classes = { "database" }
                };

                nodes.Add(node);
            }

            foreach (var notionPage in notionObjects.OfType<PageObject>().Where(nodeFilter))
            {
                var node = new Node
                {
                    Id = notionPage.Id,
                    Classes = { "page" },
                    Name = notionPage.Title
                };

                if (notionPage.Container.HasValue && _options.SetParent)
                {
                    var container = notionPage.Container.Value;
                    if (container is DatabaseObject databaseContainer && index.ContainsKey(databaseContainer.Id))
                    {
                        node.ParentId = databaseContainer.Id;
                    }
                }

                nodes.Add(node);

                foreach (var (propertyName, property) in notionPage.Properties)
                {
                    if (property is not OneToManyRelationPropertyValue relationPropertyValue)
                        continue;

                    if (!property.Configuration.HasValue || property.Configuration.Value is not RelationPropertyConfiguration relationPropertyConfiguration)
                        continue;

                    var optionalDatabase = notionCache.GetDatabase(relationPropertyConfiguration.Configuration.DatabaseId);

                    if (!optionalDatabase.HasValue || !databaseFilter(optionalDatabase.Value))
                        continue;

                    if (!databaseFilter(property.Configuration.Value.Container.Value))
                        continue;

                    foreach (var relation in relationPropertyValue.Relations)
                    {
                        if (!index.ContainsKey(relation.Id))
                            continue;

                        var edge = new Edge
                        {
                            SourceId = notionPage.Id,
                            TargetId = relation.Id
                        };

                        edges.Add(edge);
                    }
                }
            }

            return nodes.Cast<object>().Concat(edges);
        }

        private bool FilterTagDatabase(DatabaseObject databaseObject)
        {
            var databaseId = databaseObject.Id.Replace("-", "").ToLowerInvariant();

            if (!string.IsNullOrEmpty(_options.NodeSource) && databaseId.Equals(_options.NodeSource.ToLowerInvariant()))
                return true;

            return databaseId.Equals(_options.TagDatabase.ToLowerInvariant());
        }

        private bool FilterOnSource(PageObject pageObject)
        {
            if (!pageObject.Container.HasValue)
                return false;

            var containerId = pageObject.Container.Value.Id.Replace("-", "").ToLowerInvariant();

            if (containerId.Equals(_options.NodeSource.ToLowerInvariant()))
                return true;

            if (!string.IsNullOrEmpty(_options.TagDatabase))
                return containerId.Equals(_options.TagDatabase.ToLowerInvariant());

            return false;
        }
    }
}