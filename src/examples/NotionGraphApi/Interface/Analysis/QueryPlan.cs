using NotionGraphDatabase.Interface.Analysis;

namespace NotionGraphApi.Interface.Analysis;

public class QueryPlan
{
    public QuerySpecification QuerySpecification { get; }
    public List<StepDescription> PlanSteps { get; }

    public QueryPlan(QuerySpecification querySpecification, List<StepDescription> planSteps)
    {
        QuerySpecification = querySpecification;
        PlanSteps = planSteps;
    }
}