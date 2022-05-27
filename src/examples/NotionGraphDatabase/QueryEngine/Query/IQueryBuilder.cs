using NotionGraphDatabase.QueryEngine.Model;

namespace NotionGraphDatabase.QueryEngine.Query;

internal interface IQueryBuilder
{
    IQuery FromAst(QueryExpression ast);
}