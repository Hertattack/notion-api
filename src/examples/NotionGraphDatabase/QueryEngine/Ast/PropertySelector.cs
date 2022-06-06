namespace NotionGraphDatabase.QueryEngine.Ast;

internal class PropertySelector : QueryPredicate
{
    public Identifier NodeTypeIdentifier { get; protected set; }
}