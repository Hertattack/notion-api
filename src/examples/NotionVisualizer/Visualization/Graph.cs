using System.Collections.Generic;

namespace NotionVisualizer.Visualization
{
    public class Graph
    {
        public IList<Node> Nodes { get; } = new List<Node>();
        public IList<Edge> Edges { get; } = new List<Edge>();
    }
}