using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using NotionVisualizer.Visualization;
using Edge = NotionVisualizer.Generator.Cytoscape.Model.Edge;
using Node = NotionVisualizer.Generator.Cytoscape.Model.Node;

namespace NotionVisualizer.Generator.Cytoscape;

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
        _logger.LogInformation("Generating Cytoscape output to path: {outputPath}", outputPath);

        var jsFileName = Path.Join(outputPath, _cytoscapeJsFileName);
        if (!File.Exists(jsFileName))
            File.Copy(Path.Join(_cytoscapeResourcePath, _cytoscapeJsFileName), jsFileName);

        var indexHtmlFileName = Path.Join(outputPath, _indexHtmlFileName);
        if (!File.Exists(indexHtmlFileName))
            File.Copy(Path.Join(_cytoscapeResourcePath, _indexHtmlFileName), indexHtmlFileName);

        WriteJavaScriptFile(Path.Join(outputPath, _dataFileName), "data", MapGraphToOutputModel(graph));

        var configuration = new Configuration
        {
            LayoutAlgorithm = _options.LayoutAlgorithm
        };
        WriteJavaScriptFile(Path.Join(outputPath, _configurationFileName), "configuration", configuration);

        _logger.LogInformation("Generation finished.");
    }

    private IEnumerable<object> MapGraphToOutputModel(Graph graph)
    {
        var nodes = graph.Nodes
            .Select(n => new Node(n.Id, _options.SetParent ? n.ParentId : null, n.Name) {Classes = {n.Type}});

        var edges = graph.Edges
            .Select(e => new Edge(e.Id, e.SourceId, e.TargetId));

        return nodes.Cast<object>().Concat(edges);
    }

    private void WriteJavaScriptFile(string filePath, string variableName, object data)
    {
        var stringBuilder = new StringBuilder();
        stringBuilder.Append($"var {variableName} = ");
        stringBuilder.Append(
            JsonConvert.SerializeObject(data, Formatting.Indented,
                new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    DateParseHandling = DateParseHandling.None,
                    DateFormatHandling = DateFormatHandling.IsoDateFormat
                }));

        File.WriteAllText(filePath, stringBuilder.ToString());
    }
}