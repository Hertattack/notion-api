using NotionGraphDatabase.QueryEngine.Model;

namespace NotionGraphDatabase.QueryEngine.Query.Expression;

internal interface IExpressionBuilder
{
    ExpressionFunction FromAst(IQuery query, NodeClassReference nodeClassReference, string propertyName,
        Model.Expression argExpression);
}