using NotionGraphDatabase.QueryEngine.Query.Filter;

namespace NotionGraphDatabase.QueryEngine.Query.Path;

public interface ISelectStep
{
    NodeReference AssociatedNode { get; }
    IEnumerable<FilterExpression> Filter { get; }
}