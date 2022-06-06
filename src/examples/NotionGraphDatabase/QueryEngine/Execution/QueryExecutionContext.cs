namespace NotionGraphDatabase.QueryEngine.Execution;

internal class QueryExecutionContext
{
    private List<ResultContext> _contexts = new();

    private Dictionary<string, ResultContext> _contextsByAlias = new();

    public QueryExecutionContext()
    {
    }

    public ResultContext GetNextResultContext(string alias)
    {
        var context = new ResultContext(this, GetCurrentResultContext(), alias);
        _contexts.Add(context);
        _contextsByAlias.Add(alias, context);
        return context;
    }

    public ResultContext? GetCurrentResultContext()
    {
        return _contexts.LastOrDefault();
    }
}