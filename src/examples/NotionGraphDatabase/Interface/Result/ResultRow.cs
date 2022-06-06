using NotionGraphDatabase.Storage.DataModel;

namespace NotionGraphDatabase.Interface.Result;

public class ResultRow
{
    private readonly DatabasePage _databasePage;

    public ResultRow(DatabasePage databasePage)
    {
        _databasePage = databasePage;
    }
}