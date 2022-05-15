namespace NotionGraphDatabase.QueryEngine.Model;

internal class QueryAbstractSyntaxTree : IQueryAst
{
    public SelectExpression SelectExpression { get; }
    public ReturnSpecification ReturnSpecification { get; }

    public QueryAbstractSyntaxTree(SelectExpression selectExpression, ReturnSpecification returnSpecification)
    {
        SelectExpression = selectExpression;
        ReturnSpecification = returnSpecification;
    }
}