using System.Collections.Generic;
using System.IO;
using Microsoft.Extensions.Logging;
using NotionApi.Cache;
using NotionApi.Rest.Objects;

namespace NotionVisualizer.Generator
{
    public class CytoscapeGenerator : IGenerator
    {
        private const string _cytoscapeJsFileName = "cytoscape.min.js";
        private const string _indexHtmlFileName = "index.html";
        private const string _dataFileName = "data.js";
        private static readonly string _cytoscapeResourcePath = Path.Join("Resources", "cytoscape");

        private readonly ILogger<CytoscapeGenerator> _logger;

        public CytoscapeGenerator(ILogger<CytoscapeGenerator> logger)
        {
            _logger = logger;
        }

        public void Generate(string outputPath, INotionCache cache, IList<NotionObject> notionObjects)
        {
            _logger.LogInformation("Generation Cytoscape output to path: {outputPath}", outputPath);

            File.Copy(Path.Join(_cytoscapeResourcePath, _cytoscapeJsFileName), Path.Join(outputPath, _cytoscapeJsFileName));
            File.Copy(Path.Join(_cytoscapeResourcePath, _indexHtmlFileName), Path.Join(outputPath, _indexHtmlFileName));
            File.Copy(Path.Join(_cytoscapeResourcePath, _dataFileName), Path.Join(outputPath, _dataFileName));

            _logger.LogInformation("Generation finished.");
        }
    }
}