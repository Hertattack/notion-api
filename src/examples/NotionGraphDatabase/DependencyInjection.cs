using Microsoft.Extensions.DependencyInjection;
using NotionGraphDatabase.Interface;
using NotionGraphDatabase.QueryEngine;
using NotionGraphDatabase.QueryEngine.Plan;
using NotionGraphDatabase.QueryEngine.Query;
using NotionGraphDatabase.QueryEngine.Query.Expression;
using NotionGraphDatabase.QueryEngine.Query.Filter;
using NotionGraphDatabase.QueryEngine.Query.Path;
using NotionGraphDatabase.QueryEngine.Validation;
using NotionGraphDatabase.Storage;

namespace NotionGraphDatabase;

public static class DependencyInjection
{
    public static void Configure(IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<IStorageBackend, CachingNotionStorageBackend>();

        serviceCollection.AddTransient<IQueryParser, NotionQueryParser>();

        serviceCollection.AddTransient<IExpressionBuilder, ExpressionBuilder>();
        serviceCollection.AddTransient<IFilterBuilder, FilterBuilder>();
        serviceCollection.AddTransient<ISelectPathBuilder, SelectPathBuilder>();
        serviceCollection.AddTransient<IQueryBuilder, QueryBuilder>();

        serviceCollection.AddTransient<IQueryValidator, QueryValidator>();

        serviceCollection.AddTransient<IExecutionPlanBuilder, ExecutionPlanBuilder>();

        serviceCollection.AddTransient<IQueryEngine, QueryEngineImplementation>();

        serviceCollection.AddTransient<IGraphDatabase, GraphDatabase>();
    }
}