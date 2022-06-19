using NotionGraphDatabase.QueryEngine.Execution;

namespace NotionGraphApi.Interface;

public class QueryResult
{
    public IList<FieldIdentifier> PropertyNames { get; set; } = null!;

    private List<Row> _rows = new();
    public IList<Row> Rows => _rows;

    public void AddRow(Row row)
    {
        _rows.Add(row);
    }
}