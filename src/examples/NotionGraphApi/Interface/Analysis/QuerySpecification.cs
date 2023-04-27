namespace NotionGraphApi.Interface.Analysis;

public class QuerySpecification
{
    public List<QueryDatabaseReference> DatabaseReferences { get; } = new();
    public List<QueryDatabaseReference> AllPropertiesSelected { get; } = new();
    public List<OutputPropertySelection> PropertiesSelected { get; } = new();
    public List<SelectStep> SelectSteps { get; } = new();
}