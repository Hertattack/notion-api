namespace NotionGraphDatabase.QueryEngine.Query.Path;

public interface ISelectStep
{
    NodeReference AssociatedNode { get; }
}