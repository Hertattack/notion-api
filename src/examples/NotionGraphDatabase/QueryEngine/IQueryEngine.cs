using NotionGraphDatabase.QueryEngine.Model;

namespace NotionGraphDatabase.QueryEngine;

internal interface IQueryEngine
{
    QueryAbstractSyntaxTree Parse(string query);
}