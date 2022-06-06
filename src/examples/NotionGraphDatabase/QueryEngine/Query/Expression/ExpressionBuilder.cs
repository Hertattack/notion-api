using NotionGraphDatabase.QueryEngine.Ast;

namespace NotionGraphDatabase.QueryEngine.Query.Expression;

internal class ExpressionBuilder : IExpressionBuilder
{
    public ExpressionFunction FromAst(IQuery query,
        NodeClassReference nodeClassReference,
        string propertyName,
        Ast.Expression expression)
    {
        return expression switch
        {
            StringValue stringValue =>
                new StringCompareExpression(nodeClassReference.Alias.Name, propertyName, stringValue.Value),

            IntValue intValue =>
                new IntCompareExpression(nodeClassReference.Alias.Name, propertyName, intValue.Value),

            PropertyIdentifier propertyIdentifier =>
                new PropertyValueCompareExpression(
                    nodeClassReference.Alias.Name,
                    propertyName,
                    propertyIdentifier.NodeNameOrAlias.Name,
                    propertyIdentifier.PropertyName.Name),

            _ => throw new InvalidQuerySyntaxException($"Unsupported expression type: {expression.GetType().FullName}.")
        };
    }
}