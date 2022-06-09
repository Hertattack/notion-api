using NotionGraphDatabase.QueryEngine.Ast;
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
        if (returnSpecification is ReturnAllSpecification)
        {
            foreach (var nodeReference in query.NodeReferences)
                query.SetPropertySelection(new NodeAllPropertiesSelected(nodeReference));

            return;
        }

        var selectorsByAlias = new Dictionary<string, NodePropertySelection>();

        foreach (var selector in returnSpecification.Selectors)
        {
            var alias = selector.NodeIdentifier.Name;
            var nodeReference = query.FindNodeByAlias(alias);

            if (nodeReference is null)
                throw new InvalidQuerySyntaxException(
                    $"Expected node with alias: {alias} to be in the selection path but it was not found. Return specification is invalid.");

            if (selector is SelectAllProperties selectAllProperties)
            {
                selectorsByAlias[alias] = new NodeAllPropertiesSelected(nodeReference.Value);
            }
            else if (selector is SelectSpecificProperty selectSpecificProperty)
            {
                var propertyName = selectSpecificProperty.PropertyName;

                if (selectorsByAlias.TryGetValue(alias, out var existingSelector))
                {
                    if (existingSelector is NodeAllPropertiesSelected)
                        continue;

                    ((NodeSpecificPropertiesSelected) existingSelector).Add(propertyName);
                    continue;
                }

                var newSelector = new NodeSpecificPropertiesSelected(nodeReference.Value);
                newSelector.Add(propertyName);
                selectorsByAlias[alias] = newSelector;
            }
            else
            {
                throw new InvalidQuerySyntaxException($"Unsupported return specification.");
            }
        }

        foreach (var selector in selectorsByAlias.Values)
            query.SetPropertySelection(selector);
    }
}