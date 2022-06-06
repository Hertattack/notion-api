namespace NotionGraphDatabase.QueryEngine.Ast;

internal class ReturnSpecification : QueryPredicate
{
    public PropertySelector Selector { get; }

    public ReturnSpecification(PropertySelector selector)
    {
        Selector = selector;
    }

    protected ReturnSpecification()
    {
    }
}