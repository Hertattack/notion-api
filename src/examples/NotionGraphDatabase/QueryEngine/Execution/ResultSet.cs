namespace NotionGraphDatabase.QueryEngine.Execution;

public class ResultSet
{
    private List<ResultRow> _resultRows = new();
    public IEnumerable<ResultRow> Rows => _resultRows.AsReadOnly();

    public ResultRow this[string id]
    {
        get
        {
            var resultRow = _resultRows.FirstOrDefault(r => r.Key.Matches(id));

            if (resultRow is not null) return resultRow;

            resultRow = new ResultRow(new CompositeKey(id));
            _resultRows.Add(resultRow);

            return resultRow;
        }
    }
}