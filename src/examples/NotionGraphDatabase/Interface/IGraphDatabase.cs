using NotionGraphDatabase.Interface.Result;
using NotionGraphDatabase.QueryEngine;

namespace NotionGraphDatabase.Interface;

public interface IGraphDatabase
{
    QueryResult Execute(string queryText);
}