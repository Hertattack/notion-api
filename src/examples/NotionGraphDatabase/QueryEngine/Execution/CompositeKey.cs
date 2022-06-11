namespace NotionGraphDatabase.QueryEngine.Execution;

public class CompositeKey
{
    private List<string> _keys;

    public CompositeKey(string firstKey)
    {
        _keys = new List<string> {firstKey};
    }

    private CompositeKey(IEnumerable<string> keys)
    {
        _keys = keys.ToList();
    }

    public bool Matches(string id)
    {
        return _keys.Contains(id);
    }

    public void Add(string id)
    {
        if (_keys.Contains(id))
            throw new Exception($"Id '{id}' already included in composite key.");

        _keys.Add(id);
    }

    public CompositeKey Duplicate()
    {
        return new CompositeKey(_keys);
    }
}