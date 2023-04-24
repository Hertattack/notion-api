using NotionGraphDatabase.Interface.Analysis;
using NotionGraphDatabase.Interface.Result;

namespace NotionGraphDatabase.QueryEngine;

public interface IQueryEngine
{
    QueryResult Execute(string queryText);
    QueryAnalysis AnalyzeQuery(string queryText);
}