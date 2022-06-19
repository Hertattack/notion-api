namespace NotionGraphDatabase.Storage.Filtering.Integer;

public class IntNotEqualsFilterExpression : IntValueFilterExpression
{
    public IntNotEqualsFilterExpression(string nodeAlias, string propertyName, int value)
        : base(nodeAlias, propertyName, value)
    {
    }
}