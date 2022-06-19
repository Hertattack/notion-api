using System.ComponentModel;
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
        return nodeClassReference.Filter.Expressions.Select(e =>
        {
            var alias = (e.NodeIdentifier ?? nodeClassReference.Alias).Name;
            var comparisonOperator = MapOperator(e.Operator);
            var expressionFunction = _expressionBuilder.FromAst(e.Expression);

            return new FilterExpression(
                alias, e.PropertyName.Name,
                comparisonOperator,
                expressionFunction);
        });
    }

    private static ComparisonOperator MapOperator(Operator parsedOperator)
    {
        var type = parsedOperator.Type switch
        {
            OperatorType.EQUALS => ComparisonType.EQUALS,
            OperatorType.CONTAINS => ComparisonType.CONTAINS,
            OperatorType.STARTS_WITH => ComparisonType.STARTS_WITH,
            OperatorType.ENDS_WITH => ComparisonType.ENDS_WITH,
            OperatorType.GREATER_THAN => ComparisonType.GREATER_THAN,
            OperatorType.LESS_THAN => ComparisonType.LESS_THAN,
            OperatorType.GREATER_OR_EQUAL => ComparisonType.GREATER_OR_EQUAL,
            OperatorType.LESS_OR_EQUAL => ComparisonType.LESS_OR_EQUAL,
            _ => throw new InvalidEnumArgumentException($"Could not map '{parsedOperator.Type}'")
        };

        return new ComparisonOperator(type, parsedOperator.Not);
    }
}