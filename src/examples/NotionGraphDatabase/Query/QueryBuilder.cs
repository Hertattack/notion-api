using NotionGraphDatabase.Query.Path;
using NotionGraphDatabase.QueryEngine.Model;

namespace NotionGraphDatabase.Query;

internal class QueryBuilder
{
    public QueryBuilder()
    {
    }

    public IQuery FromAst(QueryAbstractSyntaxTree queryAst)
    {
        if (queryAst.SelectExpression is NodeClassReference nodeClassReference == false)
            throw new InvalidQueryException(
                $"Unsupported select expression: {queryAst.SelectExpression.GetType().FullName}.");

        var path = new QueryPath(new NodeReference(nodeClassReference.NodeIdentifier.Name,
            nodeClassReference.Alias.Name));

        var query = new QueryImplementation(path);

        if (queryAst.ReturnSpecification.Selector is SelectAllProperties selectAllProperties)
        {
            var alias = selectAllProperties.NodeTypeIdentifier.Name;
            var nodeReference = query.FindNodeByAlias(alias);

            if (nodeReference is null)
                throw new InvalidQueryException(
                    $"Expected node with alias: {alias} to be in the selection path but it was not found. Return specification is invalid.");

            query.AddPropertySelection(new NodeAllPropertiesSelected(nodeReference));
        }
        else
        {
            throw new InvalidQueryException($"Unsupported return specification.");
        }

        return query;
    }
}