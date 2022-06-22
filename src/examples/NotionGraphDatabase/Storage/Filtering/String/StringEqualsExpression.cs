namespace NotionGraphDatabase.Storage.Filtering.String;

public class StringEqualsExpression : StringValueFilterExpression
{
    public StringEqualsExpression(string nodeAlias, string propertyName, string value)
        : base(nodeAlias, propertyName, value)
    {
    }
}