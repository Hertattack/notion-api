using NotionGraphDatabase.Query.Parser.Ast;

namespace NotionGraphDatabase.QueryEngine;

internal interface IQueryParser
{
    QueryPredicate Parse(string query);
}