namespace NotionGraphDatabase.QueryEngine.Execution;

public class CompositeKey
{
    private List<DatabasePageId> _keys;

    public CompositeKey(string alias, string id)
    {
        _keys = new List<DatabasePageId> {new(alias, id)};
    }

    private CompositeKey(IEnumerable<DatabasePageId> keys)
    {
        _keys = keys.ToList();
    }

    public bool Matches(DatabasePageId key)
    {
        return _keys.Contains(key);
    }

    public void Add(DatabasePageId key)
    {
        if (_keys.Contains(key))
            throw new Exception($"Id '{key.Id}' for alias '{key.Alias}' already included in composite key.");

        _keys.Add(key);
    }

    public CompositeKey Duplicate()
    {
        return new CompositeKey(_keys);
    }
}