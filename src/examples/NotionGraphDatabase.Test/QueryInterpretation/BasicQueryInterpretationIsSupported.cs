using NUnit.Framework;

namespace NotionGraphDatabase.Test.QueryInterpretation;

internal class BasicQueryInterpretationIsSupported : QueryInterpretationTestBase
{
    [Test]
    public void Single_consistent_select_and_return_query_is_interpreted_correctly()
    {
        // Arrange
        var queryAst = queryEngine.Parse("(test) return test.*");

        // Act
        var query = _queryBuilder.FromAst(queryAst);

        // Assert
        
    }
}