using NotionGraphDatabase.Storage.DataModel;
using NotionGraphDatabase.Storage.Filtering;

namespace NotionGraphDatabase.Storage;

internal interface IStorageBackend
{
    Database? GetDatabase(string databaseId);
    bool CanPushDown(Filter expression);
}