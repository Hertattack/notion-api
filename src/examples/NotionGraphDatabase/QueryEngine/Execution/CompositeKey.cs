namespace NotionGraphDatabase.QueryEngine.Execution;

public class CompositeKey
{
    private List<string> _keys;

    public CompositeKey(string firstKey)
    {
        _keys = new List<string>();
    }

    public bool Matches(string id)
    {
        return _keys.Contains(id);
    }
}