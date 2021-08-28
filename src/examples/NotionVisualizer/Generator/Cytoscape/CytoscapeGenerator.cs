using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using NotionVisualizer.Visualization;

namespace NotionVisualizer.Generator.Cytoscape
{
    public class CytoscapeGenerator : BaseGenerator
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

        public override void Generate(string outputPath, Graph graph)
        {
            _logger.LogInformation("Generation Cytoscape output to path: {outputPath}", outputPath);

            File.Copy(Path.Join(_cytoscapeResourcePath, _cytoscapeJsFileName), Path.Join(outputPath, _cytoscapeJsFileName));
            File.Copy(Path.Join(_cytoscapeResourcePath, _indexHtmlFileName), Path.Join(outputPath, _indexHtmlFileName));

            WriteJavaScriptFile(Path.Join(outputPath, _dataFileName), "data", graph.Nodes.Cast<object>().Concat(graph.Edges));

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