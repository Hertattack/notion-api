using NotionGraphDatabase.QueryEngine.Validation;
using NotionGraphDatabase.Test.QueryInterpretation;
using NotionGraphDatabase.Test.Util;
using NUnit.Framework;

namespace NotionGraphDatabase.Test.QueryValidation;

internal class QueryValidationTestBase : QueryInterpretationTestBase
{
    protected QueryValidator _queryValidator;

    [SetUp]
    public void SetUpValidation()
    {
        var logger = new NullLogger<QueryValidator>();
        _queryValidator = new QueryValidator(logger);
    }
}