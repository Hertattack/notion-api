namespace NotionGraphDatabase.QueryEngine.Model;

internal class PropertySelector : QueryPredicate
{
    public Identifier NodeTypeIdentifier { get; protected set; }
}