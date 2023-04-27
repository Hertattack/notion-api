namespace NotionGraphDatabase.Storage.Filtering.String;

public class StringEqualsExpression : StringValueFilterExpression
{
    public StringEqualsExpression(string nodeAlias, string propertyName, string value)
        : base(nodeAlias, propertyName, value)
    {
    }
    
    public override string ToString()
    {
        return $"String Value equals filter on value: '{Value}'";
    }
}