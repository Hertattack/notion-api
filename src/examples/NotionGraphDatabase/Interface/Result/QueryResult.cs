using NotionGraphDatabase.Metadata;
using NotionGraphDatabase.QueryEngine.Execution;
using NotionGraphDatabase.QueryEngine.Query;

namespace NotionGraphDatabase.Interface.Result;

public class QueryResult
{
    public IQuery Query { get; }
    public Metamodel Metamodel { get; }
    public ResultSet ResultSet { get; }

    public QueryResult(IQuery query, Metamodel metamodel, ResultSet resultSetSet)
    {
        Query = query;
        Metamodel = metamodel;
        ResultSet = resultSetSet;
    }
}