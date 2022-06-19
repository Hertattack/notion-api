namespace NotionGraphDatabase.Storage.Filtering.Integer;

public class IntLessThanOrEqualFilterExpression : IntValueFilterExpression
{
    protected IntLessThanOrEqualFilterExpression(string nodeAlias, string propertyName, int value)
        : base(nodeAlias, propertyName, value)
    {
    }
}