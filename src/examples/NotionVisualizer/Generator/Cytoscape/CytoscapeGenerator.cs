using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using NotionApi.Cache;
using NotionApi.Rest.Objects;
using NotionVisualizer.Generator.Graph;

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
        private readonly GraphGenerator _graphGenerator;

        public CytoscapeGenerator(
            ILogger<CytoscapeGenerator> logger,
            IOptions<CytoscapeGeneratorOptions> options)
        {
            _logger = logger;
            _options = options.Value;
            _graphGenerator = new GraphGenerator(_options.NodeSource, _options.TagDatabase, _options.SetParent);
        }

        public void Generate(string outputPath, INotionCache cache, IList<NotionObject> notionObjects)
        {
            _logger.LogInformation("Generation Cytoscape output to path: {outputPath}", outputPath);

            File.Copy(Path.Join(_cytoscapeResourcePath, _cytoscapeJsFileName), Path.Join(outputPath, _cytoscapeJsFileName));
            File.Copy(Path.Join(_cytoscapeResourcePath, _indexHtmlFileName), Path.Join(outputPath, _indexHtmlFileName));

            var data = _graphGenerator.GetGraphData(cache, notionObjects);
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
    }
}