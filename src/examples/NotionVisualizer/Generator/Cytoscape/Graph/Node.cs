using System.Collections.Generic;
using Newtonsoft.Json;

namespace NotionVisualizer.Generator.Cytoscape.Graph
{
    public class Node
    {
        [JsonIgnore] public string Id { get; set; }
        [JsonIgnore] public string Name { get; set; }

        [JsonIgnore] public string ParentId { get; set; }

        [JsonProperty("group")] public string Group => "nodes";

        [JsonProperty("selectable")] public bool Selectable { get; set; } = true;

        [JsonProperty("classes")] public readonly IList<string> Classes = new List<string>();

        [JsonProperty("data")]
        public object Data => new
        {
            id = Id,
            parent = ParentId,
            label = Name
        };
    }
}