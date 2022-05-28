using NotionGraphDatabase.Metadata;
using NotionGraphDatabase.QueryEngine.Query;
using NotionGraphDatabase.Storage;

namespace NotionGraphDatabase.QueryEngine.Plan;

internal class ExecutionPlan : IQueryPlan
{
    public IQuery Query { get; }
    public Metamodel Metamodel { get; }

    public ExecutionPlan(IQuery query, Metamodel metamodel)
    {
        Query = query;
        Metamodel = metamodel;
    }

    public QueryResult Execute(IStorageBackend storageBackend)
    {
        var result = new QueryResult(Query, Metamodel);
        return result;
    }
}