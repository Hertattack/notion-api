using NotionGraphDatabase.QueryEngine.Ast;

namespace NotionGraphDatabase.QueryEngine;

internal interface IQueryParser
{
    QueryPredicate Parse(string query);
}