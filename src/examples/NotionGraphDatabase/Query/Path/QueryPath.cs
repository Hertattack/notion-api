namespace NotionGraphDatabase.Query.Path;

public class QueryPath
{
    private readonly List<IPathStep> _steps = new();

    private readonly Dictionary<string, IPathStep> _aliases = new();

    public IReadOnlyDictionary<string, IPathStep> Aliases => _aliases;

    public QueryPath(NodeReference startingPoint)
    {
        var nodePathStep = new NodePathStep(startingPoint);
        _aliases.Add(startingPoint.Alias, nodePathStep);
        _steps.Add(nodePathStep);
    }
}