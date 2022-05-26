using Newtonsoft.Json;

namespace NotionVisualizer.Generator.Cytoscape;

public class Configuration
{
    [JsonProperty("layoutName")] public string LayoutAlgorithm { get; set; }
}