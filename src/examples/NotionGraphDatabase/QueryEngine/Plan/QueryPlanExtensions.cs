using NotionGraphDatabase.Interface.Analysis;
using NotionGraphDatabase.QueryEngine.Plan.Steps;

namespace NotionGraphDatabase.QueryEngine.Plan;

internal static class QueryPlanExtensions
{
    public static QueryAnalysis ToAnalysis(this IQueryPlan plan)
    {
        var steps = plan.Steps.Select(MapStep);
        var analysis = new QueryAnalysis(plan.Query);

        return analysis;
    }

    private static StepDescription MapStep(IExecutionPlanStep planStep, int order)
    {
        var step = new StepDescription(planStep.ToString() ?? throw new Exception("Unimplemented step description."));

        return step;
    }
}