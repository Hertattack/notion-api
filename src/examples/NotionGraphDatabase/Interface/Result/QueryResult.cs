using NotionGraphDatabase.Metadata;
using NotionGraphDatabase.QueryEngine.Query;

namespace NotionGraphDatabase.Interface.Result;

public class QueryResult
{
    public IQuery Query { get; }
    public Metamodel Metamodel { get; }

    public QueryResult(IQuery query, Metamodel metamodel)
    {
        Query = query;
        Metamodel = metamodel;
    }
}