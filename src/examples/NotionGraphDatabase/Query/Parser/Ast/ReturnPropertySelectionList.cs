namespace NotionGraphDatabase.Query.Parser.Ast;

internal class ReturnPropertySelectionList : QueryPredicate
{
    private readonly List<PropertySelector> _selectors;
    public IEnumerable<PropertySelector> Selectors => _selectors;

    public ReturnPropertySelectionList(PropertySelector selector)
    {
        _selectors = new List<PropertySelector> {selector};
    }

    public void Add(PropertySelector selector)
    {
        _selectors.Add(selector);
    }
}