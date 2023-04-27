namespace NotionGraphDatabase.Storage.Filtering.String;

public class StringNotEqualsFilterExpression : StringValueFilterExpression
{
    public StringNotEqualsFilterExpression(string nodeAlias, string propertyName, string value)
        : base(nodeAlias, propertyName, value)
    {
    }
    
    public override string ToString()
    {
        return $"String Value NOT equals filter on value: '{Value}'";
    }
}