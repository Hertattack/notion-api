using NotionGraphDatabase.QueryEngine.Query.Filter;

namespace NotionGraphDatabase.QueryEngine.Query.Path;

public interface ISelectStep
{
    string Role { get; }
    NodeReference AssociatedNode { get; }
    IEnumerable<FilterExpression> Filter { get; }
    bool HasFilter { get; }
}