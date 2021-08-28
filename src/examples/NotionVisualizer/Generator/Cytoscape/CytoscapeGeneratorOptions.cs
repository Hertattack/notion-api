namespace NotionVisualizer.Generator.Cytoscape
{
    public class CytoscapeGeneratorOptions
    {
        public string LayoutAlgorithm { get; set; } = "cose";
        public bool SetParent { get; set; } = false;
    }
}