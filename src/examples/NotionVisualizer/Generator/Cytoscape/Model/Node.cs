using System.Collections.Generic;
using Newtonsoft.Json;

namespace NotionVisualizer.Generator.Cytoscape.Model
{
    public class Node
    {
        [JsonProperty("group")] public string Group => "nodes";

        [JsonProperty("selectable")] public bool Selectable { get; set; } = true;

        [JsonProperty("classes")] public readonly IList<string> Classes = new List<string>();

        [JsonProperty("data")] public object Data { get; }

        public Node(string id, string parent, string label)
        {
            Data = new
            {
                id,
                parent,
                label
            };
        }
    }
}