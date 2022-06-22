namespace NotionGraphDatabase.Storage.Filtering.String;

public class StringDoesNotContainFilterExpression : StringValueFilterExpression
{
    public StringDoesNotContainFilterExpression(string nodeAlias, string propertyName, string value)
        : base(nodeAlias, propertyName, value)
    {
    }
}