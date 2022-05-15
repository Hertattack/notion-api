using NotionGraphDatabase.QueryEngine;
using NUnit.Framework;

namespace NotionGraphDatabase.Test.QueryParsing;

[TestFixture]
internal class QueryParsingTestBase
{
    public IQueryEngine queryEngine { get; set; }

    [OneTimeSetUp]
    public void OneTimeSetup()
    {
        queryEngine = new QueryEngineImplementation();
    }
}