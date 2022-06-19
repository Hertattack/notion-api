namespace NotionGraphDatabase.Query.Parser.Ast;

internal class SelectAllProperties : PropertySelector
{
    public SelectAllProperties(Identifier identifier) : base(identifier)
    {
    }
}