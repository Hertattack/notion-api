using NotionGraphDatabase.Interface.Result;
using NotionGraphDatabase.Metadata;
using NotionGraphDatabase.QueryEngine.Execution;
using NotionGraphDatabase.QueryEngine.Query;
using NotionGraphDatabase.Storage;
using Util.Extensions;

namespace NotionGraphDatabase.QueryEngine.Plan;

internal class ExecutionPlan : IQueryPlan
{
    private Dictionary<string, Database> Databases { get; }

    private List<ExecutionPlanStep> Steps { get; } = new();

    public IQuery Query { get; }
    public Metamodel Metamodel { get; }

    public ExecutionPlan(IQuery query, Metamodel metamodel)
    {
        Query = query;
        Metamodel = metamodel;

        Databases = Metamodel.Databases.ToDictionary(d => d.Alias);

        Analyze();
    }

    private void Analyze()
    {
        foreach (var nodeReference in Query.NodeReferences)
        {
            var database = Databases[nodeReference.NodeName];
            Steps.Add(new FetchDatabaseStep(database));
        }

        foreach (var selectStepContext in Query.SelectSteps)
        {
            var currentStep = selectStepContext.Step;
            var database = Databases[currentStep.AssociatedNode.NodeName];

            if (selectStepContext.PreviousStepContext is not null)
            {
                var previousStep = selectStepContext.PreviousStepContext.ThrowIfNull();
                var role = currentStep.Role;
                var associatedNode = previousStep.Step.AssociatedNode;
                var targetDatabase = Databases[associatedNode.NodeName];
                Steps.Add(new RelationalFilterStep(role, targetDatabase));
            }

            Steps.Add(new SelectFromNodeStep(database, currentStep.AssociatedNode.Alias, currentStep.Filter));
        }

        var returnProperties = Query.ReturnPropertySelections
            .Select(CreateReturnMapping).ToList();
        Steps.Add(new CreateResultStep());
    }

    private ReturnMapping CreateReturnMapping(NodeReturnPropertySelection selection)
    {
        var nodeReference = selection.NodeReference;
        var database = Databases[nodeReference.NodeName];
        var properties = selection.SelectedProperties.Select(p => p.ReferencedNode);
        return new ReturnMapping();
    }

    public QueryResult Execute(IStorageBackend storageBackend)
    {
        var result = new QueryResult(Query, Metamodel);
        var context = new QueryExecutionContext();
        foreach (var step in Steps) step.Execute(context, storageBackend);
        return result;
    }
}