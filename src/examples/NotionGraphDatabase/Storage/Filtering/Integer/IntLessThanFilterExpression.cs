namespace NotionGraphDatabase.Storage.Filtering.Integer;

public class IntLessThanFilterExpression : IntValueFilterExpression
{
    protected IntLessThanFilterExpression(string nodeAlias, string propertyName, int value)
        : base(nodeAlias, propertyName, value)
    {
    }
}