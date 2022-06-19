namespace NotionGraphDatabase.Storage.Filtering.Integer;

public class IntEqualsFilterExpression : IntValueFilterExpression
{
    public IntEqualsFilterExpression(string nodeAlias, string propertyName, int value)
        : base(nodeAlias, propertyName, value)
    {
    }
}