namespace NotionGraphDatabase.QueryEngine.Execution;

public struct FieldIdentifier
{
    public string Alias { get; }
    public string Name { get; }

    public FieldIdentifier(string alias, string name)
    {
        Alias = alias;
        Name = name;
    }

    public override string ToString()
    {
        return $"{Alias}.'{Name}'";
    }
}