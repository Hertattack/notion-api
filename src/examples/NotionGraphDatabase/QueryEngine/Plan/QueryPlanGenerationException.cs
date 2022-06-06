namespace NotionGraphDatabase.QueryEngine.Plan;

public class QueryPlanGenerationException : Exception
{
    public QueryPlanGenerationException(string message) : base(message)
    {
    }
}