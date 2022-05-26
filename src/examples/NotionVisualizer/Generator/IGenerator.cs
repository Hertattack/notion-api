namespace NotionVisualizer.Generator;

public interface IGenerator
{
    string Name { get; }
    void Generate(string outputPath, Visualization.Graph graph);
}