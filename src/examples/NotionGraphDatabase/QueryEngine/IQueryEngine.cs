using NotionGraphDatabase.Interface.Result;
using NotionGraphDatabase.QueryEngine.Model;
using NotionGraphDatabase.QueryEngine.Query;

namespace NotionGraphDatabase.QueryEngine;

public interface IQueryEngine
{
    IQuery Parse(string queryText);
    QueryResult Execute(string queryText);
}