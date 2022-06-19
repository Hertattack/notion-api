namespace NotionGraphDatabase.Query.Parser.Ast;

internal class Identifier : QueryPredicate
{
    public string Name { get; }

    public Identifier(string identifierName)
    {
        Name = identifierName;
    }
}