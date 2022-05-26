using NotionGraphDatabase.QueryEngine.Model;
using NotionGraphDatabase.QueryEngine.Query;
using NotionGraphDatabase.QueryEngine.Query.Path;

namespace NotionGraphDatabase.QueryEngine;

internal class QueryBuilder : IQueryBuilder
{
    public IQuery FromAst(QueryExpression queryExpressionAst)
    {
        if (queryExpressionAst.SelectExpression is NodeClassReference nodeClassReference == false)
            throw new InvalidQueryException(
                $"Unsupported select expression: {queryExpressionAst.SelectExpression.GetType().FullName}.");

        var path = new QueryPath(new NodeReference(nodeClassReference.NodeIdentifier.Name,
            nodeClassReference.Alias.Name));

        var query = new QueryImplementation(path);

        if (queryExpressionAst.ReturnSpecification.Selector is SelectAllProperties selectAllProperties)
        {
            var alias = selectAllProperties.NodeTypeIdentifier.Name;
            var nodeReference = query.FindNodeByAlias(alias);

            if (nodeReference is null)
                throw new InvalidQueryException(
                    $"Expected node with alias: {alias} to be in the selection path but it was not found. Return specification is invalid.");

            query.AddPropertySelection(new NodeAllPropertiesSelected(nodeReference.Value));
        }
        else if (queryExpressionAst.ReturnSpecification is ReturnAllSpecification)
        {
            foreach (var aliasKvp in path.Aliases)
            {
                var nodeReference = query.FindNodeByAlias(aliasKvp.Key);
                query.AddPropertySelection(new NodeAllPropertiesSelected(nodeReference.GetValueOrDefault()));
            }
        }
        else
        {
            throw new InvalidQueryException($"Unsupported return specification.");
        }

        return query;
    }
}