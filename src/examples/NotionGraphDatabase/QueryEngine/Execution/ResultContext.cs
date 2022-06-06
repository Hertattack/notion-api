using NotionGraphDatabase.Interface.Result;

namespace NotionGraphDatabase.QueryEngine.Execution;

public class ResultContext
{
    private List<ResultRow> _resultRows = new();

    public ResultContext(string alias)
    {
    }

    public void AddRange(IEnumerable<ResultRow> resultRows)
    {
        _resultRows.AddRange(resultRows.ToList());
    }
}