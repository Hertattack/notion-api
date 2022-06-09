using NotionGraphDatabase.QueryEngine.Ast;

namespace NotionGraphDatabase.QueryEngine.Query.Expression;

internal class ExpressionBuilder : IExpressionBuilder
{
    public ExpressionFunction FromAst(IQuery query,
        string alias,
        string propertyName,
        Ast.Expression expression)
    {
        return expression switch
        {
            StringValue stringValue =>
                new StringCompareExpression(alias, propertyName, stringValue.Value),

            IntValue intValue =>
                new IntCompareExpression(alias, propertyName, intValue.Value),

            PropertyIdentifier propertyIdentifier =>
                new PropertyValueCompareExpression(
                    alias,
                    propertyName,
                    propertyIdentifier.NodeNameOrAlias.Name,
                    propertyIdentifier.PropertyName.Name),

            _ => throw new InvalidQuerySyntaxException($"Unsupported expression type: {expression.GetType().FullName}.")
        };
    }
}