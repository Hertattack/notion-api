using NotionGraphDatabase.Storage.DataModel;

namespace NotionGraphDatabase.Storage;

internal interface IStorageBackend
{
    Database? GetDatabase(string databaseId, bool fetchPages = true);
}