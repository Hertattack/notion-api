using NotionGraphDatabase.Interface.Result;

namespace NotionGraphDatabase.QueryEngine.Execution;

internal class ResultContext
{
    private readonly QueryExecutionContext _context;
    private readonly ResultContext? _parentContext;
    private readonly string _alias;
    private List<ResultRow> _resultRows = new();

    public ResultContext(QueryExecutionContext context, ResultContext? parentContext, string alias)
    {
        _context = context;
        _parentContext = parentContext;
        _alias = alias;
    }

    public void AddRange(IEnumerable<ResultRow> resultRows)
    {
        _resultRows.AddRange(resultRows.ToList());
    }

    public IEnumerable<DenormalizedResultRow> DenormalizeRows()
    {
        var rows = new List<DenormalizedResultRow>();

        foreach (var resultRow in _resultRows)
        {
        }

        return rows;
    }
}