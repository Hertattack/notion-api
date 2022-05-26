using Microsoft.Extensions.DependencyInjection;
using NotionGraphDatabase.QueryEngine;

namespace NotionGraphDatabase;

public static class DependencyInjection
{
    public static void Configure(IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<IQueryParser, NotionQueryParser>();
        serviceCollection.AddTransient<IQueryBuilder, QueryBuilder>();
        serviceCollection.AddTransient<IQueryEngine, QueryEngineImplementation>();
    }
}