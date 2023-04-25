using NotionGraphDatabase.Query;

namespace NotionGraphDatabase.Interface.Analysis;

public class QueryAnalysis
{
    public IQuery Query { get; }
    public List<StepDescription> Steps { get; }

    public QueryAnalysis(IQuery forQuery, IEnumerable<StepDescription> steps)
    {
        Query = forQuery;
        Steps = steps.ToList();
    }
}