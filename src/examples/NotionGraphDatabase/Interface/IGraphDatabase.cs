using NotionGraphDatabase.Interface.Result;
using NotionGraphDatabase.QueryEngine;
using NotionGraphDatabase.Storage.DataModel;

namespace NotionGraphDatabase.Interface;

public interface IGraphDatabase
{
    QueryResult Execute(string queryText); 
    DatabaseDefinition GetDatabaseDefinition(string databaseId);
}