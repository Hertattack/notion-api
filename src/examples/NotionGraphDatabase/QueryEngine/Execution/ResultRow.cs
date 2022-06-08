namespace NotionGraphDatabase.QueryEngine.Execution;

public class ResultRow
{
    public CompositeKey Key { get; }

    private Dictionary<string, object?> _values = new();

    public ResultRow(CompositeKey key)
    {
        Key = key;
    }

    public object? this[string propertyName]
    {
        get => _values[propertyName];
        set => _values[propertyName] = value;
    }
}