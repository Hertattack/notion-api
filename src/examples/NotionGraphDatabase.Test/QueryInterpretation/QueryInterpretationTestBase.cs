using NotionGraphDatabase.QueryEngine.Query;
using NotionGraphDatabase.QueryEngine.Query.Expression;
using NotionGraphDatabase.QueryEngine.Query.Filter;
using NotionGraphDatabase.QueryEngine.Query.Path;
using NotionGraphDatabase.Test.QueryParsing;
using NotionGraphDatabase.Test.Util;
using NUnit.Framework;

namespace NotionGraphDatabase.Test.QueryInterpretation;

internal class QueryInterpretationTestBase : QueryParsingTestBase
{
    protected QueryBuilder? _queryBuilder;

    [SetUp]
    public void SetUp()
    {
        var expressionBuilder = new ExpressionBuilder();
        var filterBuilder = new FilterBuilder(expressionBuilder);
        var selectPathBuilder = new SelectPathBuilder(filterBuilder);
        _queryBuilder = new QueryBuilder(selectPathBuilder, new SubstituteLogger<QueryBuilder>());
    }
}