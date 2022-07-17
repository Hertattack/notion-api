using NotionGraphDatabase.Storage.DataModel;

namespace NotionGraphDatabase.QueryEngine.Execution;

public class IntermediateResultRow
{
    private readonly DatabasePage _databasePage;

    private readonly List<IntermediateResultRow> _parentRecords;
    public IEnumerable<IntermediateResultRow> ParentRows => _parentRecords.AsReadOnly();

    public IEnumerable<string> PropertyNames =>
        _databasePage.Properties.Select(p => p.Name).Prepend("Id");

    public string Id => _databasePage.Id;

    public IntermediateResultRow(DatabasePage databasePage)
    {
        _databasePage = databasePage;
        _parentRecords = new List<IntermediateResultRow>();
    }

    public IntermediateResultRow(DatabasePage databasePage, IEnumerable<IntermediateResultRow> parentRecords)
    {
        _databasePage = databasePage;
        _parentRecords = parentRecords.ToList();
    }

    public object? this[string propertyName] => propertyName == "Id" ? _databasePage.Id : _databasePage[propertyName];

    public IntermediateResultRow GetParentByAlias(string alias)
    {
        throw new Exception("Chain of results not supported yet.");
    }
}