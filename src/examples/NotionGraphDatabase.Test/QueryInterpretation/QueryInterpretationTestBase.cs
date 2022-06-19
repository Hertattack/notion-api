using NotionGraphDatabase.Query;
using NotionGraphDatabase.Query.Expression;
using NotionGraphDatabase.Query.Filter;
using NotionGraphDatabase.Query.Path;
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