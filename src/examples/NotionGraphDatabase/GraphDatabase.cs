using NotionApi;
using NotionGraphDatabase.Interface;
using NotionGraphDatabase.Metadata;
using NotionGraphDatabase.QueryEngine;

namespace NotionGraphDatabase;

public class GraphDatabase : IGraphDatabase
{
    private readonly Model _model;
    private readonly INotionClient _notionClient;
    private readonly IQueryEngine _queryEngine;

    public GraphDatabase(Model model, INotionClient notionClient)
    {
        _model = model;
        _notionClient = notionClient;
        _queryEngine = new QueryEngineImplementation();
    }
}