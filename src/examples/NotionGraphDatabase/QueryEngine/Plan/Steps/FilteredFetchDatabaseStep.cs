using NotionGraphDatabase.Metadata;
using NotionGraphDatabase.QueryEngine.Execution;
using NotionGraphDatabase.QueryEngine.Query.Expression;
using NotionGraphDatabase.QueryEngine.Query.Filter;
using NotionGraphDatabase.Storage;
using NotionGraphDatabase.Storage.Filtering;
using NotionGraphDatabase.Util;
using Util.Extensions;

namespace NotionGraphDatabase.QueryEngine.Plan.Steps;

internal class FilteredFetchDatabaseStep : ExecutionPlanStep
{
    private readonly Database _database;
    private readonly DatabaseFilter _filter = new();

    private readonly List<List<FilterExpression>> _orFilters = new();

    public FilteredFetchDatabaseStep(Database database)
    {
        _database = database;
    }

    public override void Execute(QueryExecutionContext executionContext, IStorageBackend storageBackend)
    {
        var database = storageBackend.GetDatabase(_database.Id.RemoveDashes()).ThrowIfNull();

        var databaseFilter = new DatabaseFilter();
        foreach (var filterSet in _orFilters)
            databaseFilter.Or(filterSet.Select(f => MapToDatabaseFilterCondition(database, f)));

        database.GetFiltered(_filter);
    }

    public override string ToString()
    {
        return $"Fetch database with filter '{_database.Id}' ({_database.Alias})";
    }

    public void AddOrCondition(IEnumerable<FilterExpression> expressions)
    {
        _orFilters.Add(expressions.ToList());
    }

    private static DatabaseFilterCondition MapToDatabaseFilterCondition(
        Storage.DataModel.Database database,
        FilterExpression expression)
    {
        var propertyDefinition = database.GetProperty(expression.PropertyName);
        if (expression.Expression is StringCompareExpression stringCompareExpression)
            return new StringFilter(propertyDefinition, stringCompareExpression.Value);

        var typeName = expression.Expression.GetType().FullName;
        throw new Exception(
            $"Cannot translate query filter to database filter. Filter type: '{typeName}', property type: '{propertyDefinition.Type}'");
    }
}