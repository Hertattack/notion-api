namespace NotionGraphDatabase.QueryEngine.Model;

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