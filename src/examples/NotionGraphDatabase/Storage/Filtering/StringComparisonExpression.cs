namespace NotionGraphDatabase.Storage.Filtering;

public class StringComparisonExpression : PropertyFilterExpression
{
    public string Value { get; }

    public StringComparisonExpression(string nodeAlias, string propertyName, string value)
        : base(nodeAlias, propertyName)
    {
        Value = value;
    }
}