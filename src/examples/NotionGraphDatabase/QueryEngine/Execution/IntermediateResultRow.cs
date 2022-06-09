using NotionGraphDatabase.Storage.DataModel;

namespace NotionGraphDatabase.QueryEngine.Execution;

public class IntermediateResultRow
{
    private readonly DatabasePage _databasePage;
    private readonly IEnumerable<IntermediateResultRow>? _parentRecords;

    public IEnumerable<string> PropertyNames =>
        _databasePage.Properties.Select(p => p.Name);

    public string Id => _databasePage.Id;

    public IntermediateResultRow(DatabasePage databasePage)
    {
        _databasePage = databasePage;
    }

    public IntermediateResultRow(DatabasePage databasePage, IEnumerable<IntermediateResultRow> parentRecords)
    {
        _databasePage = databasePage;
        _parentRecords = parentRecords;
    }

    public object? this[string propertyName] => _databasePage[propertyName];

    public IntermediateResultRow GetParentByAlias(string alias)
    {
        throw new Exception("Chain of results not supported yet.");
    }
}