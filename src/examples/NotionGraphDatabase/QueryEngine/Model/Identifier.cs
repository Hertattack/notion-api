namespace NotionGraphDatabase.QueryEngine.Model;

internal class Identifier : QueryPredicate
{
    public string Name { get; }

    public Identifier(string identifierName)
    {
        Name = identifierName;
    }
}