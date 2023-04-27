namespace NotionGraphApi.Interface.Analysis;

public class QueryFilterExpression
{
    public string ReferencedAlias { get; }
    public string Description { get; }

    public QueryFilterExpression(string referencedAlias, string description)
    {
        ReferencedAlias = referencedAlias;
        Description = description;
    }
}