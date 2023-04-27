namespace NotionGraphApi.Interface.Analysis;

public class SelectStep
{
    public QueryDatabaseReference DatabaseReference { get; }
    public string Alias { get; }
    public List<QueryFilterExpression> Filters { get; } = new();

    public SelectStep(QueryDatabaseReference databaseReference, string? stepRole)
    {
        DatabaseReference = databaseReference;
        Alias = stepRole ?? databaseReference.QueryAlias;
    }
}