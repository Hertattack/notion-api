namespace NotionGraphDatabase.QueryEngine.Ast;

internal class PropertyIdentifier : Expression
{
    public Identifier NodeNameOrAlias { get; }
    public Identifier PropertyName { get; }

    public PropertyIdentifier(Identifier nodeNameOrAlias, Identifier propertyName)
    {
        NodeNameOrAlias = nodeNameOrAlias;
        PropertyName = propertyName;
    }
}