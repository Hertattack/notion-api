using NotionGraphDatabase.Metadata;
using NotionGraphDatabase.QueryEngine.Execution;
using NotionGraphDatabase.QueryEngine.Query;

namespace NotionGraphDatabase.Interface.Result;

public class QueryResult
{
    public IQuery Query { get; }
    public Metamodel Metamodel { get; }
    public ResultSet Result { get; }

    public QueryResult(IQuery query, Metamodel metamodel, ResultSet resultSet)
    {
        Query = query;
        Metamodel = metamodel;
        Result = resultSet;
    }
}