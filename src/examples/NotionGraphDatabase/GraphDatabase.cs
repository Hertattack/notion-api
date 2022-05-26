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

    public GraphDatabase(
        IMetamodelFactory modelFactory,
        INotionClient notionClient,
        IQueryEngine queryEngine)
    {
        _model = modelFactory.CreateModel();
        _notionClient = notionClient;
        _queryEngine = queryEngine;
    }
}