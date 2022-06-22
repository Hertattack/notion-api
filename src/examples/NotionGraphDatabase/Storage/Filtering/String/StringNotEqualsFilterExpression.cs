namespace NotionGraphDatabase.Storage.Filtering.String;

public class StringNotEqualsFilterExpression : StringValueFilterExpression
{
    public StringNotEqualsFilterExpression(string nodeAlias, string propertyName, string value)
        : base(nodeAlias, propertyName, value)
    {
    }
}