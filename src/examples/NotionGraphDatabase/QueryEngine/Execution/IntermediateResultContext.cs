namespace NotionGraphDatabase.QueryEngine.Execution;

internal class IntermediateResultContext
{
    private readonly QueryExecutionContext _context;
    private readonly IntermediateResultContext? _parentContext;

    private readonly string _alias;
    public string Alias => _alias;

    private List<IntermediateResultRow> _resultRows = new();
    public IEnumerable<IntermediateResultRow> IntermediateResultRows => _resultRows.AsReadOnly();

    public IntermediateResultContext(QueryExecutionContext context, IntermediateResultContext? parentContext,
        string alias)
    {
        _context = context;
        _parentContext = parentContext;
        _alias = alias;
    }

    public void AddRange(IEnumerable<IntermediateResultRow> resultRows)
    {
        _resultRows.AddRange(resultRows.ToList());
    }
}