using NotionGraphApi.Interface.Analysis;
using NotionGraphDatabase.Interface.Analysis;

namespace NotionGraphApi.Mapping;

public class QueryAnalysisMapper
{
    public QueryPlan Map(QueryAnalysis analysis)
    {
        var plan = new QueryPlan(analysis.Query, analysis.Steps);
        return plan;
    }
}