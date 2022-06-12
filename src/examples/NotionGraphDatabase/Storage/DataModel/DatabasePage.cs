using NotionApi.Rest.Response.Page;
using NotionGraphDatabase.Storage.Mappers;
using NotionGraphDatabase.Util;

namespace NotionGraphDatabase.Storage.DataModel;

public class DatabasePage : Page
{
    private Database? _database;
    private PageObject _notionObject = null!;

    public DatabasePage(Database database, PageObject pageObject, DateTime lastEditTime)
    {
        _database = database;
        Id = pageObject.Id.RemoveDashes();

        Update(pageObject, lastEditTime);
    }

    public string Id { get; }
    public DateTime LastEditTimestamp { get; private set; }

    public IEnumerable<PropertyDefinition> Properties =>
        _database?.Properties ?? Array.Empty<PropertyDefinition>();

    public void Update(PageObject notionObject, DateTime lastEditTime)
    {
        if (_database is null)
            throw new StorageException("Page is deleted.");

        if (notionObject.Id.RemoveDashes() != Id)
            throw new StorageException("Cannot assign a new object with a different Id.");

        _notionObject = notionObject;

        LastEditTimestamp = lastEditTime;
    }

    public void Delete()
    {
        _database = null;
    }

    public object? this[string propertyName]
    {
        get
        {
            if (_notionObject.Properties.ContainsKey(propertyName))
                return PropertyValueMapper.Map(_notionObject.Properties[propertyName]);

            throw new UndefinedPropertyException(propertyName);
        }
    }
}