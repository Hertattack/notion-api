using NotionGraphDatabase.QueryEngine.Ast;

namespace NotionGraphDatabase.QueryEngine.Query;

internal interface IQueryBuilder
{
    IQuery FromAst(QueryExpression ast);
}