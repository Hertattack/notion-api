using NotionGraphDatabase.Interface.Analysis;
using NotionGraphDatabase.Query;

namespace NotionGraphApi.Interface.Analysis;

public class QueryPlan
{
    public IQuery Query { get; }

    public QueryPlan(IQuery query, List<StepDescription> stepDescriptions)
    {
        Query = query;
    }
}