using NotionApi;
using NotionGraphDatabase.Interface;
using NotionGraphDatabase.Metadata;

namespace NotionGraphDatabase;

public class GraphDatabase : IGraphDatabase
{
    private readonly Model _model;
    private readonly INotionClient _notionClient;

    public GraphDatabase(Model model, INotionClient notionClient)
    {
        _model = model;
        _notionClient = notionClient;
    }
}