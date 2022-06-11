using NotionGraphDatabase.Storage.DataModel;
using NotionGraphDatabase.Util;

namespace NotionGraphDatabase.Storage;

public class DataStore
{
    private Dictionary<string, Database> _databases = new();

    public Database GetDatabase(string databaseId)
    {
        var unifiedGuid = databaseId.RemoveDashes();

        if (_databases.ContainsKey(databaseId))
            return _databases[databaseId];

        var definition = new Database(this, databaseId);
        _databases[databaseId] = definition;

        return definition;
    }
}