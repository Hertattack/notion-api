using NotionGraphDatabase.Metadata;
using NotionGraphDatabase.QueryEngine.Plan.Filtering;
using NotionGraphDatabase.Storage.Filtering;

namespace NotionGraphDatabase.QueryEngine.Plan.Steps;

internal abstract class SelectStep : ExecutionPlanStep
{
    protected readonly Database _database;
    protected readonly string _alias;
    protected readonly PropertyValueResolver _resolver;
    protected readonly FilterEngine _filterEngine;

    protected SelectStep(Database database, string alias, Filter? filter)
    {
        _database = database;
        _alias = alias;
        _resolver = new PropertyValueResolver();
        _filterEngine = new FilterEngine(_resolver, filter);
    }
}