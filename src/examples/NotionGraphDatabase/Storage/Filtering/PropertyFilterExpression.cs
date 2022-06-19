namespace NotionGraphDatabase.Storage.Filtering;

public class PropertyFilterExpression : Filter
{
    public string NodeAlias { get; }
    public string PropertyName { get; }

    protected PropertyFilterExpression(string nodeAlias, string propertyName)
    {
        NodeAlias = nodeAlias;
        PropertyName = propertyName;
    }
}