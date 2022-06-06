using NotionGraphDatabase.QueryEngine.Ast;
using NotionGraphDatabase.QueryEngine.Query.Expression;

namespace NotionGraphDatabase.QueryEngine.Query.Filter;

internal class FilterBuilder : IFilterBuilder
{
    private readonly IExpressionBuilder _expressionBuilder;

    public FilterBuilder(IExpressionBuilder expressionBuilder)
    {
        _expressionBuilder = expressionBuilder;
    }

    public IEnumerable<FilterExpression> FromAst(IQuery query, NodeClassReference nodeClassReference)
    {
        var nodeReference = new NodeReference(nodeClassReference.NodeIdentifier.Name, nodeClassReference.Alias.Name);
        return nodeClassReference.Filter.Expressions.Select(e =>
            new FilterExpression(
                query,
                nodeReference,
                e.PropertyIdentifier.Name,
                _expressionBuilder.FromAst(query, nodeClassReference, e.PropertyIdentifier.Name, e.Expression)));
    }
}