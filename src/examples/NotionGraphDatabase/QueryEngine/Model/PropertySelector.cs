namespace NotionGraphDatabase.QueryEngine.Model;

internal class PropertySelector : IQueryAst
{
    public Identifier NodeTypeIdentifier { get; protected set; }
}