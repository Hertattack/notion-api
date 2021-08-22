using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using NotionApi.Cache;
using NotionApi.Rest.Database;
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

            var data = BuildData(notionObjects);
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

        private IEnumerable<object> BuildData(IList<NotionObject> notionObjects)
        {
            var index = notionObjects.ToDictionary(n => n.Id);

            var nodes = new List<Node>();
            var edges = new List<Edge>();

            foreach (var notionPage in notionObjects.OfType<DatabaseObject>())
            {
                var node = new Node
                {
                    Id = notionPage.Id,
                    Classes = { "database" }
                };

                nodes.Add(node);
            }

            foreach (var notionPage in notionObjects.OfType<PageObject>())
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

                    if (!property.Configuration.HasValue || !property.Configuration.Value.Container.HasValue)
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
    }
}