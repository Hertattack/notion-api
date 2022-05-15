namespace NotionGraphDatabase.Query;

public interface IPathStep
{
    NodeReference AssociatedNode { get; }
}