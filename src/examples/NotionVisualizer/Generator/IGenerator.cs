namespace NotionVisualizer.Generator
{
    public interface IGenerator
    {
        string Name { get; }
        void Generate(string outputPath, NotionVisualizer.Visualization.Graph graph);
    }
}