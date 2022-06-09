namespace NotionGraphDatabase.QueryEngine.Query.Expression;

internal interface IExpressionBuilder
{
    ExpressionFunction FromAst(
        IQuery query,
        string alias,
        string propertyName,
        Ast.Expression argExpression);
}