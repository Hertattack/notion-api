namespace NotionGraphDatabase.Query.Parser.Ast;

internal class PropertyName : QueryPredicate
{
    public string Name { get; }

    public PropertyName(string name)
    {
        Name = name;
    }
}