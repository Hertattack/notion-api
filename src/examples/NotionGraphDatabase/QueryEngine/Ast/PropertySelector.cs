namespace NotionGraphDatabase.QueryEngine.Ast;

internal abstract class PropertySelector : QueryPredicate
{
    protected PropertySelector(Identifier nodeIdentifier)
    {
        NodeIdentifier = nodeIdentifier;
    }

    public Identifier NodeIdentifier { get; }
}