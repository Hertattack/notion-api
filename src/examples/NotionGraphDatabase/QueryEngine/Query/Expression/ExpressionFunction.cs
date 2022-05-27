namespace NotionGraphDatabase.QueryEngine.Query.Expression;

public abstract class ExpressionFunction
{
    public abstract bool Matches(object value);
}