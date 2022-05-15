namespace NotionGraphDatabase.QueryEngine.Model;

internal class Identifier : IQueryAst
{
    public string Name { get; }

    public Identifier(string identifierName)
    {
        Name = identifierName;
    }
}