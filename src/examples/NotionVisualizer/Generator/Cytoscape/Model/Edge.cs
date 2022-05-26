using Newtonsoft.Json;

namespace NotionVisualizer.Generator.Cytoscape.Model;

public class Edge
{
    [JsonProperty("data")] public object Data { get; }

    public Edge(string id, string source, string target)
    {
        Data = new {id, source, target};
    }
}