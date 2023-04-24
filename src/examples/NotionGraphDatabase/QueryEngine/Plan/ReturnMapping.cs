using NotionGraphDatabase.Metadata;

namespace NotionGraphDatabase.QueryEngine.Plan;

internal class ReturnMapping
{
    public Database Database { get; }
    public string Alias { get; }
    public bool AllSelected { get; set; } = false;

    private HashSet<string> _propertyNames = new();
    public IEnumerable<string> PropertyNames => _propertyNames;

    public ReturnMapping(Database database, string alias)
    {
        Database = database;
        Alias = alias;
    }

    public ReturnMapping(Database database, string alias, IEnumerable<string> propertyNames)
    {
        Database = database;
        Alias = alias;
        AllSelected = false;
        _propertyNames = propertyNames.ToHashSet();
    }

    public override string ToString()
    {
        if (AllSelected)
            return $"[{Alias}].*";

        return string.Join(", ", _propertyNames.Select(p => $"[{Alias}].[{p}]"));
    }
}