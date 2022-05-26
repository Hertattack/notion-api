using System;

namespace NotionVisualizer.Generator;

public abstract class BaseGenerator : IGenerator
{
    public string Name { get; }

    protected BaseGenerator()
    {
        var name = GetType().Name;
        var generatorIndex = name.IndexOf("Generator", StringComparison.Ordinal);

        Name = generatorIndex > -1 ? name[..generatorIndex] : name;
    }

    public abstract void Generate(string outputPath, Visualization.Graph graph);
}