using NotionApi;
using NotionGraphDatabase.Interface;
using NotionGraphDatabase.QueryEngine;

namespace NotionGraphDatabase;

public class GraphDatabase : IGraphDatabase
{
    private readonly INotionClient _notionClient;
    private readonly IQueryEngine _queryEngine;

    public GraphDatabase(
        INotionClient notionClient,
        IQueryEngine queryEngine)
    {
        _notionClient = notionClient;
        _queryEngine = queryEngine;
    }
}