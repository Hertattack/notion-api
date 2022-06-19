namespace NotionGraphDatabase.Query.Parser.Ast;

internal class FilterExpressionList : QueryPredicate
{
    private readonly List<FilterExpression> _expressions;
    public IEnumerable<FilterExpression> Expressions => _expressions;

    public FilterExpressionList()
    {
        _expressions = new List<FilterExpression>();
    }

    public FilterExpressionList(FilterExpression expression)
    {
        _expressions = new List<FilterExpression> {expression};
    }

    public FilterExpressionList(IEnumerable<FilterExpression> expressions)
    {
        _expressions = expressions.ToList();
    }
}