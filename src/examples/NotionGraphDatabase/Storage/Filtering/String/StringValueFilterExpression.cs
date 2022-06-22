namespace NotionGraphDatabase.Storage.Filtering.String;

public class StringValueFilterExpression : PropertyFilterExpression
{
    public string Value { get; }

    protected StringValueFilterExpression(string nodeAlias, string propertyName, string value) : base(nodeAlias,
        propertyName)
    {
        Value = value;
    }
}