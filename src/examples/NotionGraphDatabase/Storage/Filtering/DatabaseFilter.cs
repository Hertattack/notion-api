namespace NotionGraphDatabase.Storage.Filtering;

public class DatabaseFilter
{
    private List<List<DatabaseFilterCondition>> _conditions = new();

    public void Or(IEnumerable<DatabaseFilterCondition> conditions)
    {
        _conditions.Add(conditions.ToList());
    }
}