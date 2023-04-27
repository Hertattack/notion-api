namespace NotionGraphDatabase.Storage.Filtering.String;

public class StringContainsFilterExpression : StringValueFilterExpression
{
    public StringContainsFilterExpression(string nodeAlias, string propertyName, string value)
        : base(nodeAlias, propertyName, value)
    {
    }

    public override string ToString()
    {
        return $"String Value filter, contains value: '{Value}'";
    }
}