using NotionGraphDatabase.Storage.DataModel;

namespace NotionGraphDatabase.QueryEngine.Execution;

public class IntermediateResultRow
{
    private readonly DatabasePage _databasePage;

    public IEnumerable<string> PropertyNames =>
        _databasePage.Properties.Select(p => p.Name);

    public string Id => _databasePage.Id;

    public IntermediateResultRow(DatabasePage databasePage)
    {
        _databasePage = databasePage;
    }

    public object? this[string propertyName] => _databasePage[propertyName];
}