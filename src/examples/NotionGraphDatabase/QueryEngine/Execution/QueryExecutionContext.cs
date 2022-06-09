using NotionGraphDatabase.Metadata;
using NotionGraphDatabase.Storage.DataModel;

namespace NotionGraphDatabase.QueryEngine.Execution;

internal class QueryExecutionContext
{
    public Metamodel Metamodel { get; }
    private List<IntermediateResultContext> _contexts = new();

    private Dictionary<string, IntermediateResultContext> _contextsByAlias = new();

    public QueryExecutionContext(Metamodel metamodel)
    {
        Metamodel = metamodel;
    }

    public ResultSet ResultSet { get; } = new();

    public IntermediateResultContext GetNextResultContext(
        IEnumerable<PropertyDefinition> propertyDefinitions,
        string alias)
    {
        var context = new IntermediateResultContext(this, GetCurrentResultContext(), alias, propertyDefinitions);
        _contexts.Add(context);
        _contextsByAlias.Add(alias, context);
        return context;
    }

    public IntermediateResultContext? GetCurrentResultContext()
    {
        return _contexts.LastOrDefault();
    }
}