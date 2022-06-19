using NotionGraphDatabase.Query.Filter;

namespace NotionGraphDatabase.Query.Path;

public class NodeSelectStep : ISelectStep
{
    public NodeReference AssociatedNode { get; }
    public string? Role { get; }

    private readonly List<FilterExpression> _filter;
    public IEnumerable<FilterExpression> Filter => _filter.AsReadOnly();
    public bool HasFilter => _filter.Any();

    public NodeSelectStep(
        NodeReference nodeReference,
        IEnumerable<FilterExpression> filter,
        string? role)
    {
        AssociatedNode = nodeReference;
        Role = role;
        _filter = filter.ToList();
    }
}