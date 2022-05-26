using NotionGraphDatabase.QueryEngine.Model;

namespace NotionGraphDatabase.QueryEngine;

internal interface IQueryParser
{
    QueryPredicate Parse(string query);
}