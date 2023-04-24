using NotionGraphDatabase.Query;

namespace NotionGraphDatabase.Interface.Analysis;

public class QueryAnalysis
{
    public IQuery Query { get; }

    public QueryAnalysis(IQuery forQuery)
    {
        Query = forQuery;
    }
}