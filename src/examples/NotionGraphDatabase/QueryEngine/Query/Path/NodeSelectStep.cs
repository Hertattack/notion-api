using NotionGraphDatabase.QueryEngine.Query.Filter;

namespace NotionGraphDatabase.QueryEngine.Query.Path;

public class NodeSelectStep : ISelectStep
{
    private readonly NodeReference _nodeReference;

    public NodeSelectStep(NodeReference nodeReference, IEnumerable<FilterExpression> filter)
    {
        _nodeReference = nodeReference;
    }

    public NodeReference AssociatedNode => _nodeReference;
}