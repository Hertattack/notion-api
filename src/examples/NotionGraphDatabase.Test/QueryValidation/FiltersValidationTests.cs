using FluentAssertions;
using NotionGraphDatabase.QueryEngine.Ast;
using NUnit.Framework;

namespace NotionGraphDatabase.Test.QueryValidation;

internal class FiltersValidationTests : QueryValidationTestBase
{
    [Test]
    public void Validation_should_fail_on_undefined_alias_in_filter()
    {
        // Arrange
        const string queryString = "(test{prop=f.prop})";
        var queryAst = _queryParser.Parse(queryString).As<QueryExpression>();
        var query = _queryBuilder.FromAst(queryAst);

        // Act
        var validationResult = _queryValidator.Validate(query);

        // Assert
        validationResult.IsInvalid.Should().BeFalse();
    }

    [Test]
    public void Validation_should_fail_on_alias_in_filter_defined_in_same_node()
    {
        // Arrange
        const string queryString = "(t:test{prop=t.prop})";
        var queryAst = _queryParser.Parse(queryString).As<QueryExpression>();
        var query = _queryBuilder.FromAst(queryAst);

        // Act
        var validationResult = _queryValidator.Validate(query);

        // Assert
        validationResult.IsInvalid.Should().BeFalse();
    }

    [Test]
    public void Validation_should_no_fail_on_defined_alias_in_filter()
    {
        // Arrange
        const string queryString = "(t:test)-[role]->(other{prop=t.prop})";
        var queryAst = _queryParser.Parse(queryString).As<QueryExpression>();
        var query = _queryBuilder.FromAst(queryAst);

        // Act
        var validationResult = _queryValidator.Validate(query);

        // Assert
        validationResult.IsInvalid.Should().BeFalse();
    }
}