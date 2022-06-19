namespace NotionGraphDatabase.Storage.Filtering;

public class StringEqualsExpression : StringValueFilterExpression
{
    public StringEqualsExpression(string nodeAlias, string propertyName, string value)
        : base(nodeAlias, propertyName, value)
    {
    }
}