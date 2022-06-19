namespace NotionGraphDatabase.Query.Expression;

internal class PropertyIdentifier : Expression
{
    public string Alias { get; }

    public string PropertyName { get; }

    public PropertyIdentifier(string alias, string propertyName)
    {
        Alias = alias;
        PropertyName = propertyName;
    }

    public override string ToString()
    {
        return $"Property Identifier: {Alias}.{PropertyName}";
    }
}