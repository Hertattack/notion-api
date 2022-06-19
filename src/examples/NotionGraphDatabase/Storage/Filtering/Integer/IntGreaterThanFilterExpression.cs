namespace NotionGraphDatabase.Storage.Filtering.Integer;

public class IntGreaterThanFilterExpression : IntValueFilterExpression
{
    public IntGreaterThanFilterExpression(string nodeAlias, string propertyName, int value) 
        : base(nodeAlias, propertyName, value)
    {
    }
}