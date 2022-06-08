namespace NotionGraphDatabase.QueryEngine.Execution;

internal class QueryExecutionContext
{
    private List<IntermediateResultContext> _contexts = new();

    private Dictionary<string, IntermediateResultContext> _contextsByAlias = new();

    public ResultSet ResultSet { get; } = new();

    public IntermediateResultContext GetNextResultContext(string alias)
    {
        var context = new IntermediateResultContext(this, GetCurrentResultContext(), alias);
        _contexts.Add(context);
        _contextsByAlias.Add(alias, context);
        return context;
    }

    public IntermediateResultContext? GetCurrentResultContext()
    {
        return _contexts.LastOrDefault();
    }
}