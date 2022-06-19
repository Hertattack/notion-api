using NotionGraphDatabase.Query.Expression;
using NotionGraphDatabase.Query.Filter;
using NotionGraphDatabase.QueryEngine.Execution.Filtering;
using NotionGraphDatabase.Storage.Filtering;
using NotionGraphDatabase.Storage.Filtering.Integer;

namespace NotionGraphDatabase.QueryEngine.Plan;

internal class FilterExpressionBuilder
{
    private List<Func<Filter?, Filter>> _stack = new();

    public static Filter Build(FilterExpression expression)
    {
        return expression.Expression switch
        {
            IntegerExpression intCompareExpression =>
                CreateIntegerFilter(expression, intCompareExpression),
            StringExpression stringCompareExpression =>
                CreateStringFilter(expression, stringCompareExpression),
            PropertyIdentifier propertyCompareExpression =>
                CreatePropertyFilter(expression, propertyCompareExpression),
            _ => throw new Exception($"Unsupported filter expression: '{expression.GetType().FullName}'")
        };
    }

    private static PropertyComparisonExpression CreatePropertyFilter(FilterExpression expression,
        PropertyIdentifier propertyCompareExpression)
    {
        return new PropertyComparisonExpression(
            expression.Alias, expression.PropertyName,
            propertyCompareExpression.Alias, propertyCompareExpression.PropertyName);
    }

    private static StringEqualsExpression CreateStringFilter(FilterExpression expression,
        StringExpression stringExpression)
    {
        return new StringEqualsExpression(expression.Alias, expression.PropertyName,
            stringExpression.Value);
    }

    private static Filter CreateIntegerFilter(
        FilterExpression expression,
        IntegerExpression integerExpression)
    {
        var alias = expression.Alias;
        if (expression.Operator.Type == ComparisonType.EQUALS)
            return expression.Operator.IsNegated
                ? new IntEqualsFilterExpression(alias, expression.PropertyName, integerExpression.Value)
                : new IntNotEqualsFilterExpression(alias, expression.PropertyName,
                    integerExpression.Value);

        if (expression.Operator.IsNegated)
            throw new Exception($"Operator {expression.Operator.Type} cannot be negated");

        if (expression.Operator.Type == ComparisonType.GREATER_THAN)
            return new IntGreaterThanFilterExpression(alias, expression.PropertyName,
                integerExpression.Value);

        throw new Exception($"Cannot map filter expression from query: '{integerExpression}'");
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