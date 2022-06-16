using Microsoft.Extensions.Logging;
using NotionGraphDatabase.QueryEngine.Ast;
using NotionGraphDatabase.QueryEngine.Query.Path;

namespace NotionGraphDatabase.QueryEngine.Query;

internal class QueryBuilder : IQueryBuilder
{
    private readonly ISelectPathBuilder _selectPathBuilder;
    private readonly ILogger<QueryBuilder> _logger;

    public QueryBuilder(
        ISelectPathBuilder selectPathBuilder,
        ILogger<QueryBuilder> logger)
    {
        _selectPathBuilder = selectPathBuilder;
        _logger = logger;
    }

    public IQuery FromAst(QueryExpression queryExpressionAst)
    {
        _logger.LogDebug("Building query from AST");

        var query = new QueryImplementation();

        _selectPathBuilder.FromAst(query, queryExpressionAst.SelectExpression);

        SelectReturnPropertiesFromReturnSpecification(query, queryExpressionAst.ReturnSpecification);

        _logger.LogDebug("Query built from AST");
        return query;
    }

    private void SelectReturnPropertiesFromReturnSpecification(
        QueryImplementation query,
        ReturnSpecification returnSpecification)
    {
        _logger.LogDebug("Building return part of query from AST");

        if (returnSpecification is ReturnAllSpecification)
        {
            _logger.LogDebug("Return all properties for all nodes in query");
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

            switch (selector)
            {
                case SelectAllProperties:
                    _logger.LogDebug("Return all properties for node with alias: {Alias}", alias);
                    selectorsByAlias[alias] = new NodeAllPropertiesSelected(nodeReference.Value);
                    break;
                case SelectSpecificProperty selectSpecificProperty:
                {
                    var propertyName = selectSpecificProperty.PropertyName;
                    _logger.LogDebug("Select property: {PropertyName} from node with alias: {Alias}",
                        propertyName, alias);

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
                    break;
                }
                default:
                    throw new InvalidQuerySyntaxException("Unsupported return specification.");
            }

            _logger.LogDebug("Finished building return part of query from AST");
        }

        foreach (var selector in selectorsByAlias.Values)
            query.SetPropertySelection(selector);
    }
}