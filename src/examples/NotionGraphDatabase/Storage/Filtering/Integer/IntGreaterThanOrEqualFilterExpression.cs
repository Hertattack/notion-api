namespace NotionGraphDatabase.Storage.Filtering.Integer;

public class IntGreaterThanOrEqualFilterExpression : IntValueFilterExpression
{
    protected IntGreaterThanOrEqualFilterExpression(string nodeAlias, string propertyName, int value)
        : base(nodeAlias, propertyName, value)
    {
    }
}