namespace NotionGraphDatabase.Storage.Filtering;

public class ExpressionGroup : Filter
{
    private List<Filter> _orList = new();

    public void Add(Filter expression)
    {
        _orList.Add(expression);
    }
}