using NotionGraphDatabase.QueryEngine.Model;

namespace NotionGraphDatabase.QueryEngine.Query.Path;

internal interface ISelectPathBuilder
{
    void FromAst(IQuery query, SelectExpression selectExpression);
}