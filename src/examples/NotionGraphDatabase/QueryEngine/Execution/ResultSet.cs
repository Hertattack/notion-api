namespace NotionGraphDatabase.QueryEngine.Execution;

public class ResultSet
{
    private List<ResultRow> _resultRows = new();
    public IEnumerable<ResultRow> Rows => _resultRows.AsReadOnly();

    public IEnumerable<ResultRow> this[DatabasePageId key]
    {
        get
        {
            var resultRows = _resultRows.Where(r => r.Key.Matches(key)).ToList();

            if (resultRows.Any()) return resultRows;

            return Array.Empty<ResultRow>();
        }
    }

    public void AddRow(ResultRow newRow)
    {
        _resultRows.Add(newRow);
    }
}