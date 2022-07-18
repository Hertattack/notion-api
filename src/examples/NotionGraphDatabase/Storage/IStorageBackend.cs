using NotionGraphDatabase.Storage.DataModel;
using NotionGraphDatabase.Storage.Filtering;

namespace NotionGraphDatabase.Storage;

public interface IStorageBackend
{
    Database? GetDatabase(string databaseId);
    bool Supports(Filter filter);
    DatabaseDefinition GetDatabaseDefinition(string databaseId);
}