namespace NotionGraphDatabase.Query.Parser.Ast;

internal class Operator : QueryPredicate
{
    public OperatorType Type { get; }
    public bool IsNegated { get; }
    public Operator Negate => new(Type, true);

    public Operator(OperatorType type, bool negate = false)
    {
        Type = type;
        IsNegated = negate;
    }
}