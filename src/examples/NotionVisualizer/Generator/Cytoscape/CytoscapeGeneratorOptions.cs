namespace NotionVisualizer.Generator.Cytoscape
{
    public class CytoscapeGeneratorOptions
    {
        public bool SetParent { get; set; }

        public string LayoutAlgorithm { get; set; } = "cose";

        public string TagDatabase { get; set; }

        public string NodeSource { get; set; }
    }
}