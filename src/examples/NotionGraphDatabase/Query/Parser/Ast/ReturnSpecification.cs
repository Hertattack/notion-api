namespace NotionGraphDatabase.Query.Parser.Ast;

internal class ReturnSpecification : QueryPredicate
{
    private readonly List<PropertySelector> _selectors = new();

    public IEnumerable<PropertySelector> Selectors => _selectors;

    public ReturnSpecification(ReturnPropertySelectionList returnPropertySelection)
    {
        _selectors.AddRange(returnPropertySelection.Selectors);
    }

    protected ReturnSpecification()
    {
    }
}