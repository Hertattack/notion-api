namespace NotionGraphDatabase.Query;

public readonly struct NodeReference
{
    public string NodeName { get; }
    public string Alias { get; }

    public NodeReference(string nodeName, string alias)
    {
        NodeName = nodeName;
        Alias = alias;
    }

    public override bool Equals(object? obj)
    {
        return base.Equals(obj);
    }

    public bool Equals(NodeReference other)
    {
        return NodeName == other.NodeName && Alias == other.Alias;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(NodeName, Alias);
    }

    public static bool operator ==(NodeReference left, NodeReference right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(NodeReference left, NodeReference right)
    {
        return !(left == right);
    }
}