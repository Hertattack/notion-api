using NotionGraphDatabase.Query.Filter;

namespace NotionGraphDatabase.Query.Path;

public interface ISelectStep
{
    string Role { get; }
    NodeReference AssociatedNode { get; }
    IEnumerable<FilterExpression> Filter { get; }
    bool HasFilter { get; }
}