using NotionGraphDatabase.Storage.DataModel;
using NotionGraphDatabase.Util;

namespace NotionGraphDatabase.Storage;

public class DataStore
{
    private Dictionary<string, Database> _databases = new();

    public Database CreateOrRetrieveDatabase(string databaseId, string title)
    {
        var unifiedGuid = databaseId.RemoveDashes();

        if (_databases.ContainsKey(databaseId))
            return _databases[databaseId];

        var definition = new Database(this, databaseId, title);
        _databases[databaseId] = definition;

        return definition;
    }
}