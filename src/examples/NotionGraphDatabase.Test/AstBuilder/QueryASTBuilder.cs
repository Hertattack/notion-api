using NotionGraphDatabase.QueryEngine.Model;

namespace NotionGraphDatabase.Test.AstBuilder;

public class QueryAstBuilder : IQueryAstBuilder
{
    private readonly SelectContext _selectContext;
    private readonly ReturnContext _returnContext;

    public QueryAstBuilder()
    {
        _selectContext = new SelectContext(this);
        _returnContext = new ReturnContext(this);
    }

    public SelectContext Selecting => _selectContext;
    public ReturnContext Returning => _returnContext;

    public IQueryAst Build()
    {
        var selectExpression = (SelectExpression) _selectContext.Build();
        var returnExpression = (ReturnSpecification) _returnContext.Build();

        return new QueryEngine.Model.QueryAbstractSyntaxTree(selectExpression, returnExpression);
    }
}