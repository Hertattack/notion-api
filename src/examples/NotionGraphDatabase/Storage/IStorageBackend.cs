using NotionGraphDatabase.Storage.DataModel;

namespace NotionGraphDatabase.Storage;

internal interface IStorageBackend
{
    Database? GetDatabase(string databaseId, bool retrieveAllPages = true);
}