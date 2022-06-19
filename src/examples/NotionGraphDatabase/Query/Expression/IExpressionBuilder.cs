namespace NotionGraphDatabase.Query.Expression;

internal interface IExpressionBuilder
{
    Expression FromAst(Parser.Ast.Expression astExpression);
}