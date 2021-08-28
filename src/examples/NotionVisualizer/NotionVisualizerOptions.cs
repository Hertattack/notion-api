using System.Collections.Generic;

namespace NotionVisualizer
{
    public class NotionVisualizerOptions
    {
        public IList<string> Databases { get; set; } = new List<string>();

        public IList<string> UseGenerators { get; set; } = new List<string> { "Cytoscape" };


        public IList<EdgeDirection> EdgeDirections { get; set; } = new List<EdgeDirection>();

        public bool SetParent { get; set; }
    }
}