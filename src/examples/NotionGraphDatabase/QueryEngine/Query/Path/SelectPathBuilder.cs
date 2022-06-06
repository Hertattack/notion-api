using NotionGraphDatabase.QueryEngine.Ast;
using NotionGraphDatabase.QueryEngine.Query.Filter;

namespace NotionGraphDatabase.QueryEngine.Query.Path;

internal class SelectPathBuilder : ISelectPathBuilder
{
    private readonly IFilterBuilder _filterBuilder;

    public SelectPathBuilder(IFilterBuilder filterBuilder)
    {
        _filterBuilder = filterBuilder;
    }

    public void FromAst(IQuery query, SelectExpression selectExpression)
    {
        var currentStep = selectExpression;
        string? lastRole = null;
        while (currentStep is SelectPathExpression pathExpression)
        {
            if (pathExpression.FromExpression is not NodeClassReference fromExpression)
                throw new InvalidQuerySyntaxException(
                    $"Unsupported select expression: {pathExpression.FromExpression.GetType().FullName}.");

            var fromNodeReference = new NodeReference(fromExpression.NodeIdentifier.Name, fromExpression.Alias.Name);
            var fromFilter = _filterBuilder.FromAst(query, fromExpression);
            query.AddNextSelectStep(new NodeSelectStep(fromNodeReference, fromFilter, lastRole));
            lastRole = pathExpression.ViaRole.Name;
            currentStep = pathExpression.ToExpression;
        }

        if (currentStep is not NodeClassReference nodeClassReference)
            throw new InvalidQuerySyntaxException(
                $"Unsupported select expression: {currentStep.GetType().FullName}.");

        var filter = _filterBuilder.FromAst(query, nodeClassReference);
        var nodeReference = new NodeReference(nodeClassReference.NodeIdentifier.Name, nodeClassReference.Alias.Name);
        query.AddNextSelectStep(new NodeSelectStep(nodeReference, filter, lastRole));
    }
}