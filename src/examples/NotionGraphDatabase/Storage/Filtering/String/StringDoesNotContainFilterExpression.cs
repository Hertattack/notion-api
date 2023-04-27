namespace NotionGraphDatabase.Storage.Filtering.String;

public class StringDoesNotContainFilterExpression : StringValueFilterExpression
{
    public StringDoesNotContainFilterExpression(string nodeAlias, string propertyName, string value)
        : base(nodeAlias, propertyName, value)
    {
    }
    
    public override string ToString()
    {
        return $"String Value filter, does NOT contain value: '{Value}'";
    }
}