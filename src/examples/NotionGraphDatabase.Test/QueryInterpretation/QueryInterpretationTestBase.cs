using NotionGraphDatabase.QueryEngine;
using NotionGraphDatabase.Test.QueryParsing;
using NUnit.Framework;

namespace NotionGraphDatabase.Test.QueryInterpretation;

[TestFixture]
internal class QueryInterpretationTestBase : QueryParsingTestBase
{
    protected QueryBuilder _queryBuilder;

    [SetUp]
    public void SetUp()
    {
        _queryBuilder = new QueryBuilder();
    }
}