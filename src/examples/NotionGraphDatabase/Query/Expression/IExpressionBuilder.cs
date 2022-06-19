namespace NotionGraphDatabase.Query.Expression;

internal interface IExpressionBuilder
{
    ExpressionFunction FromAst(
        IQuery query,
        string alias,
        string propertyName,
        Parser.Ast.Expression argExpression);
}