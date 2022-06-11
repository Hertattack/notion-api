namespace NotionGraphDatabase.QueryEngine.Execution;

public class ResultSet
{
    private List<ResultRow> _resultRows = new();
    public IEnumerable<ResultRow> Rows => _resultRows.AsReadOnly();

    public IEnumerable<ResultRow> this[string id]
    {
        get
        {
            var resultRows = _resultRows.Where(r => r.Key.Matches(id)).ToList();

            if (resultRows.Any()) return resultRows;

            return Array.Empty<ResultRow>();
        }
    }

    public void AddRow(ResultRow newRow)
    {
        _resultRows.Add(newRow);
    }
}