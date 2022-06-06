using NotionApi.Rest.Response.Page;

namespace NotionGraphDatabase.Storage.DataModel;

public class DatabasePage : Page
{
    private Database? _database;
    private PageObject _notionObject = null!;

    public DatabasePage(Database database, PageObject pageObject)
    {
        _database = database;
        Id = pageObject.Id;

        Update(pageObject);
    }

    public string Id { get; }
    public DateTime LastEditTimestamp { get; set; }

    public void Update(PageObject notionObject)
    {
        if (_database is null)
            throw new StorageException("Page is deleted.");

        if (notionObject.Id != Id)
            throw new StorageException("Cannot assign a new object with a different Id.");

        _notionObject = notionObject;

        LastEditTimestamp = Convert.ToDateTime(_notionObject.LastEditedTime);
    }

    public void Delete()
    {
        _database = null;
    }
}