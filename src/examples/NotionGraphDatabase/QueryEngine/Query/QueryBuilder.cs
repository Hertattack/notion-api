using NotionGraphDatabase.QueryEngine.Model;
using NotionGraphDatabase.QueryEngine.Query.Path;

namespace NotionGraphDatabase.QueryEngine.Query;

internal class QueryBuilder : IQueryBuilder
{
    private readonly ISelectPathBuilder _selectPathBuilder;

    public QueryBuilder(ISelectPathBuilder selectPathBuilder)
    {
        _selectPathBuilder = selectPathBuilder;
    }

    public IQuery FromAst(QueryExpression queryExpressionAst)
    {
        var query = new QueryImplementation();
        _selectPathBuilder.FromAst(query, queryExpressionAst.SelectExpression);
        SelectReturnPropertiesFromReturnSpecification(query, queryExpressionAst.ReturnSpecification);

        return query;
    }

    private static void SelectReturnPropertiesFromReturnSpecification(
        QueryImplementation query,
        ReturnSpecification returnSpecification)
    {
        if (returnSpecification.Selector is SelectAllProperties selectAllProperties)
        {
            var alias = selectAllProperties.NodeTypeIdentifier.Name;
            var nodeReference = query.FindNodeByAlias(alias);

            if (nodeReference is null)
                throw new InvalidQuerySyntaxException(
                    $"Expected node with alias: {alias} to be in the selection path but it was not found. Return specification is invalid.");

            query.AddPropertySelection(new NodeAllPropertiesSelected(nodeReference.Value));
        }
        else if (returnSpecification is ReturnAllSpecification)
        {
            foreach (var nodeReference in query.NodeReferences)
                query.AddPropertySelection(new NodeAllPropertiesSelected(nodeReference));
        }
        else
        {
            throw new InvalidQuerySyntaxException($"Unsupported return specification.");
        }
    }
}