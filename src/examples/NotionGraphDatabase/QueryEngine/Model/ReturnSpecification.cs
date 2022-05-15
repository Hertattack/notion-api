namespace NotionGraphDatabase.QueryEngine.Model;

internal class ReturnSpecification : IQueryAst
{
    public PropertySelector Selector { get; }

    public ReturnSpecification(PropertySelector selector)
    {
        Selector = selector;
    }
}