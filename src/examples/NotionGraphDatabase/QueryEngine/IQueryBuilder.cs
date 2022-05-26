using NotionGraphDatabase.QueryEngine.Model;
using NotionGraphDatabase.QueryEngine.Query;

namespace NotionGraphDatabase.QueryEngine;

internal interface IQueryBuilder
{
    IQuery FromAst(QueryExpression ast);
}