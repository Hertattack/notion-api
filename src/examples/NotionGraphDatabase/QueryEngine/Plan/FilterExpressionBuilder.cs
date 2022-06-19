using NotionGraphDatabase.Query.Expression;
using NotionGraphDatabase.Query.Filter;
using NotionGraphDatabase.QueryEngine.Execution.Filtering;
using NotionGraphDatabase.Storage.Filtering;

namespace NotionGraphDatabase.QueryEngine.Plan;

internal class FilterExpressionBuilder
{
    private List<Func<Filter?, Filter>> _stack = new();

    public static Filter Build(FilterExpression expression)
    {
        return expression.Expression switch
        {
            IntCompareExpression intCompareExpression =>
                new IntComparisonExpression(expression.Alias, expression.PropertyName, intCompareExpression.Value),
            StringCompareExpression stringCompareExpression =>
                new StringComparisonExpression(expression.Alias, expression.PropertyName,
                    stringCompareExpression.Value),
            PropertyValueCompareExpression propertyCompareExpression =>
                new PropertyComparisonExpression(
                    expression.Alias, expression.PropertyName,
                    propertyCompareExpression.RightAlias, propertyCompareExpression.RightPropertyName),
            _ => throw new Exception($"Unsupported filter expression: '{expression.GetType().FullName}'")
        };
    }

    public Filter Build()
    {
        Filter? currentExpression = null;

        foreach (var stackFrame in _stack)
            currentExpression = stackFrame(currentExpression);

        return currentExpression ?? new EmptyFilterExpression();
    }

    public FilterExpressionBuilder Or(FilterExpressionBuilder builder)
    {
        _stack.Add(CreateGroupFilter<OrExpressionRepresentation>(builder.Build));
        return this;
    }


    public static FilterExpressionBuilder And(IEnumerable<FilterExpression> expressions)
    {
        var builder = new FilterExpressionBuilder();
        foreach (var expression in expressions)
            builder.And(expression);
        return builder;
    }

    public FilterExpressionBuilder And(FilterExpression expression)
    {
        _stack.Add(CreateGroupFilter<AndExpressionRepresentation>(() => Build(expression)));
        return this;
    }

    private static Func<Filter?, Filter> CreateGroupFilter<TFilterType>(
        Func<Filter> expressionGenerator)
        where TFilterType : ExpressionGroup
    {
        return previousRepresentation =>
        {
            var expression = expressionGenerator();

            switch (previousRepresentation)
            {
                case null:
                    return expression;
                case TFilterType expressionGroup:
                    expressionGroup.Add(expression);
                    return previousRepresentation;
                default:
                    var newExpressionGroup = Activator.CreateInstance<TFilterType>();
                    newExpressionGroup.Add(previousRepresentation);
                    newExpressionGroup.Add(expression);

                    return newExpressionGroup;
            }
        };
    }
}