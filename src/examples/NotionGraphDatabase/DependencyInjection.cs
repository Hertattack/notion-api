using Microsoft.Extensions.DependencyInjection;
using NotionGraphDatabase.QueryEngine;
using NotionGraphDatabase.QueryEngine.Query;
using NotionGraphDatabase.QueryEngine.Query.Expression;
using NotionGraphDatabase.QueryEngine.Query.Filter;
using NotionGraphDatabase.QueryEngine.Query.Path;

namespace NotionGraphDatabase;

public static class DependencyInjection
{
    public static void Configure(IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<IQueryParser, NotionQueryParser>();

        serviceCollection.AddTransient<IExpressionBuilder, ExpressionBuilder>();
        serviceCollection.AddTransient<IFilterBuilder, FilterBuilder>();
        serviceCollection.AddTransient<ISelectPathBuilder, SelectPathBuilder>();
        serviceCollection.AddTransient<IQueryBuilder, QueryBuilder>();

        serviceCollection.AddTransient<IQueryEngine, QueryEngineImplementation>();
    }
}