using Newtonsoft.Json;

namespace NotionVisualizer.Generator.Cytoscape.Graph
{
    public class Edge
    {
        [JsonIgnore] public string Id { get; set; }
        [JsonIgnore] public string SourceId { get; set; }
        [JsonIgnore] public string TargetId { get; set; }


        [JsonProperty("data")]
        public object Data => new
        {
            id = Id,
            source = SourceId,
            target = TargetId
        };
    }
}