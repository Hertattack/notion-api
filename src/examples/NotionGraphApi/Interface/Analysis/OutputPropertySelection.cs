namespace NotionGraphApi.Interface.Analysis;

public class OutputPropertySelection
{
    public QueryDatabaseReference DatabaseReference { get; }

    public List<string> PropertyNames { get; }

    public OutputPropertySelection(QueryDatabaseReference databaseReference, IEnumerable<string> propertyNames)
    {
        DatabaseReference = databaseReference;
        PropertyNames = propertyNames.ToList();
    }
}