namespace NotionGraphDatabase.Query.Parser.Ast;

internal class Operator : QueryPredicate
{
    public OperatorType Type { get; }
    public bool Not { get; }
    public Operator Negate => new(Type, true);

    public Operator(OperatorType type, bool negate = false)
    {
        Type = type;
        Not = !negate;
    }
}