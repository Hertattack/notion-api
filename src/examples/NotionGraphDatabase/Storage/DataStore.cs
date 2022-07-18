using Microsoft.Extensions.Logging;
using NotionApi;
using NotionGraphDatabase.Storage.DataModel;

namespace NotionGraphDatabase.Storage;

public class DataStore
{
    private readonly INotionClient _notionClient;
    private readonly ILogger<Database> _databaseLogger;

    private Dictionary<string, Database> _databases = new();

    public DataStore(
        INotionClient notionClient,
        ILogger<Database> databaseLogger)
    {
        _notionClient = notionClient;
        _databaseLogger = databaseLogger;
    }

    public Database CreateOrRetrieveDatabase(DatabaseDefinition databaseDefinition)
    {
        lock (this)
        {
            if (_databases.ContainsKey(databaseDefinition.Id))
            {
                var existingDatabase = _databases[databaseDefinition.Id];
                existingDatabase.UpdateDefinition(databaseDefinition);
                return existingDatabase;
            }

            var newDatabase = new Database(databaseDefinition, _notionClient, _databaseLogger);
            _databases[databaseDefinition.Id] = newDatabase;

            return newDatabase;
        }
    }
}