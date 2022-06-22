namespace NotionGraphDatabase.Storage.Filtering.String;

public class StringContainsFilterExpression : StringValueFilterExpression
{
    public StringContainsFilterExpression(string nodeAlias, string propertyName, string value)
        : base(nodeAlias, propertyName, value)
    {
    }
}