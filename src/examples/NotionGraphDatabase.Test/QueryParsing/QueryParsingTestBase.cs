using NotionGraphDatabase.QueryEngine;
using NUnit.Framework;

namespace NotionGraphDatabase.Test.QueryParsing;

[TestFixture]
internal class QueryParsingTestBase
{
    public IQueryParser _queryParser { get; set; }

    [OneTimeSetUp]
    public void OneTimeSetup()
    {
        _queryParser = new NotionQueryParser();
    }
}