namespace NotionGraphDatabase.QueryEngine.Query.Path;

public interface IPathStep
{
    NodeReference AssociatedNode { get; }
}