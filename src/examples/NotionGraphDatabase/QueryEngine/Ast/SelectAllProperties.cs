namespace NotionGraphDatabase.QueryEngine.Ast;

internal class SelectAllProperties : PropertySelector
{
    public SelectAllProperties(Identifier identifier) : base(identifier)
    {
    }
}