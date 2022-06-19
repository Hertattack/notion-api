using NotionGraphDatabase.Query.Parser.Ast;

namespace NotionGraphDatabase.Query;

internal interface IQueryBuilder
{
    IQuery FromAst(QueryExpression ast);
}