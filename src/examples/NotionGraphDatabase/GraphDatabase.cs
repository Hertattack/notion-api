using NotionGraphDatabase.Interface;
using NotionGraphDatabase.Interface.Result;
using NotionGraphDatabase.QueryEngine;

namespace NotionGraphDatabase;

public class GraphDatabase : IGraphDatabase
{
    private readonly IQueryEngine _queryEngine;

    public GraphDatabase(IQueryEngine queryEngine)
    {
        _queryEngine = queryEngine;
    }

    public QueryResult Execute(string queryText)
    {
        return _queryEngine.Execute(queryText);
    }
}