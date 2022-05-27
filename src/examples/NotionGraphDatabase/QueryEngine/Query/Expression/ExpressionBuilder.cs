using NotionGraphDatabase.QueryEngine.Model;

namespace NotionGraphDatabase.QueryEngine.Query.Expression;

internal class ExpressionBuilder : IExpressionBuilder
{
    public ExpressionFunction FromAst(
        IQuery query,
        NodeClassReference nodeClassReference,
        Model.Expression expression)
    {
        return expression switch
        {
            StringValue stringValue =>
                new StringCompareExpression(stringValue.Value),

            IntValue intValue =>
                new IntCompareExpression(intValue.Value),

            PropertyIdentifier propertyIdentifier =>
                new PropertyValueCompareExpression(
                    propertyIdentifier.NodeNameOrAlias.Name,
                    propertyIdentifier.PropertyName.Name),

            _ => throw new InvalidQueryException($"Unsupported expression type: {expression.GetType().FullName}.")
        };
    }
}