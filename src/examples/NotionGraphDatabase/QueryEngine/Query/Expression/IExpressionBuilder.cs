using NotionGraphDatabase.QueryEngine.Ast;

namespace NotionGraphDatabase.QueryEngine.Query.Expression;

internal interface IExpressionBuilder
{
    ExpressionFunction FromAst(IQuery query, NodeClassReference nodeClassReference, string propertyName,
        Ast.Expression argExpression);
}