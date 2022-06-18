using Microsoft.Extensions.Logging;
using NotionApi;
using NotionGraphDatabase.Storage.DataModel;
using NotionGraphDatabase.Util;

namespace NotionGraphDatabase.Storage;

public class DataStore
{
    private readonly INotionClient _notionClient;
    private readonly ILoggerFactory _loggerFactory;

    private Dictionary<string, Database> _databases = new();

    public DataStore(INotionClient notionClient, ILoggerFactory loggerFactory)
    {
        _notionClient = notionClient;
        _loggerFactory = loggerFactory;
    }

    public Database CreateOrRetrieveDatabase(string databaseId)
    {
        var unifiedGuid = databaseId.RemoveDashes();

        lock (this)
        {
            if (_databases.ContainsKey(unifiedGuid))
                return _databases[unifiedGuid];

            var definition = new Database(databaseId, _notionClient, _loggerFactory.CreateLogger<Database>());
            _databases[unifiedGuid] = definition;

            definition.UpdateDefinition();
            return definition;
        }
    }
}