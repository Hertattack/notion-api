using NotionGraphDatabase.Query.Parser.Ast;

namespace NotionGraphDatabase.Query.Expression;

internal class ExpressionBuilder : IExpressionBuilder
{
    public Expression FromAst(Parser.Ast.Expression expression)
    {
        return expression switch
        {
            StringValue stringValue =>
                new StringExpression(stringValue.Value),

            IntValue intValue =>
                new IntegerExpression(intValue.Value),

            Parser.Ast.PropertyIdentifier propertyIdentifier =>
                new PropertyIdentifier(
                    propertyIdentifier.NodeNameOrAlias.Name,
                    propertyIdentifier.PropertyName.Name),

            _ => throw new InvalidQuerySyntaxException($"Unsupported expression type: {expression.GetType().FullName}.")
        };
    }
}