namespace NotionGraphDatabase.QueryEngine.Execution;

public class ResultRow
{
    public CompositeKey Key { get; }
    public IEnumerable<FieldIdentifier> PropertyNames => _values.Keys;
    public string LastEditTime { get; }

    private Dictionary<FieldIdentifier, object?> _values = new();

    public ResultRow(CompositeKey key)
    {
        Key = key;
    }

    public object? this[FieldIdentifier fieldId]
    {
        get => _values[fieldId];
        set => _values[fieldId] = value;
    }

    public ResultRow Duplicate()
    {
        var duplicate = new ResultRow(Key.Duplicate());
        foreach (var dictionaryEntry in _values) duplicate[dictionaryEntry.Key] = dictionaryEntry.Value;
        return duplicate;
    }
}