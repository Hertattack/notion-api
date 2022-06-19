using NotionGraphDatabase.Query.Expression;
using NotionGraphDatabase.Query.Parser.Ast;

namespace NotionGraphDatabase.Query.Filter;

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
        {
            var alias = (e.NodeIdentifier ?? nodeClassReference.Alias).Name;
            return new FilterExpression(
                query,
                alias,
                e.PropertyName.Name,
                _expressionBuilder.FromAst(query, alias, e.PropertyName.Name, e.Expression));
        });
    }
}