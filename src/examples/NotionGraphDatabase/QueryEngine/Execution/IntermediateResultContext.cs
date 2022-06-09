using NotionGraphDatabase.Storage.DataModel;

namespace NotionGraphDatabase.QueryEngine.Execution;

internal class IntermediateResultContext
{
    private readonly QueryExecutionContext _context;
    private readonly IntermediateResultContext? _parentContext;

    private readonly string _alias;
    public string Alias => _alias;

    private List<IntermediateResultRow> _resultRows = new();
    private readonly List<PropertyDefinition> _propertyDefinitions;
    public IEnumerable<PropertyDefinition> PropertyDefinitions => _propertyDefinitions.AsReadOnly();
    public IEnumerable<IntermediateResultRow> IntermediateResultRows => _resultRows.AsReadOnly();

    public IntermediateResultContext(QueryExecutionContext context, IntermediateResultContext? parentContext,
        string alias, IEnumerable<PropertyDefinition> propertyDefinitions)
    {
        _context = context;
        _parentContext = parentContext;
        _alias = alias;
        _propertyDefinitions = propertyDefinitions.ToList();
    }

    public void AddRange(IEnumerable<IntermediateResultRow> resultRows)
    {
        _resultRows.AddRange(resultRows.ToList());
    }

    public IEnumerable<string> SelectedAliases()
    {
        var result = new List<string> {Alias};

        var previous = _parentContext;
        while (previous is not null)
        {
            result.Add(previous.Alias);
            previous = previous._parentContext;
        }

        return result;
    }

    public IReadOnlyDictionary<string, IntermediateResultContext> GetSelectedContextsByAlias()
    {
        var result = new Dictionary<string, IntermediateResultContext> {{Alias, this}};

        var previous = _parentContext;
        while (previous is not null)
        {
            result[previous.Alias] = previous;
            previous = previous._parentContext;
        }

        return result;
    }
}