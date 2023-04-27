using NotionGraphApi.Interface.Analysis;
using NotionGraphDatabase.Interface.Analysis;
using NotionGraphDatabase.Query;
using NotionGraphDatabase.Query.Filter;

namespace NotionGraphApi.Mapping;

public class QueryAnalysisMapper
{
    public QueryPlan Map(QueryAnalysis analysis)
    {
        var plan = new QueryPlan(Map(analysis.Query), analysis.Steps);
        return plan;
    }

    public QuerySpecification Map(IQuery query)
    {
        var spec = new QuerySpecification();

        spec.DatabaseReferences.AddRange(query.NodeReferences.Select(Map));

        var propertySelections = query.ReturnPropertySelections.Select(r => r.PropertySelection).ToList();
        spec.AllPropertiesSelected.AddRange(propertySelections.OfType<NodeAllPropertiesSelected>().Select(Map));
        spec.PropertiesSelected.AddRange(propertySelections.OfType<NodeSpecificPropertiesSelected>().Select(Map));

        spec.SelectSteps.AddRange(query.SelectSteps.Select(Map));
        return spec;
    }

    public SelectStep Map(ISelectStepContext stepContext)
    {
        var databaseReference = Map(stepContext.Step.AssociatedNode);
        var step = new SelectStep(databaseReference, stepContext.Step.Role);

        step.Filters.AddRange(stepContext.Step.Filter.Select(Map));

        return step;
    }

    public QueryFilterExpression Map(FilterExpression filterExpression)
    {
        return new QueryFilterExpression(filterExpression.Alias, filterExpression.ToString());
    }

    public QueryDatabaseReference Map(NodeAllPropertiesSelected allPropertiesSelected)
    {
        return Map(allPropertiesSelected.ReferencedNode);
    }

    public OutputPropertySelection Map(NodeSpecificPropertiesSelected specificPropertiesSelected)
    {
        return new OutputPropertySelection(Map(specificPropertiesSelected.ReferencedNode),
            specificPropertiesSelected.PropertyNames);
    }

    public QueryDatabaseReference Map(NodeReference reference)
    {
        return new QueryDatabaseReference {DatabaseAlias = reference.NodeName, QueryAlias = reference.Alias};
    }
}