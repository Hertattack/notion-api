using FluentAssertions;
using NotionGraphDatabase.Metadata;
using NotionGraphDatabase.Query.Parser.Ast;
using NUnit.Framework;
using Util.Extensions;

namespace NotionGraphDatabase.Test.QueryValidation;

internal class FiltersValidationTests : QueryValidationTestBase
{
    [Test]
    public void Validation_should_fail_on_undefined_alias_in_filter()
    {
        // Arrange
        var model = new Metamodel();
        const string queryString = "(test{prop=f.prop})";
        var queryAst = _queryParser.ThrowIfNull().Parse(queryString).As<QueryExpression>();
        var query = _queryBuilder.ThrowIfNull().FromAst(queryAst);

        // Act
        var validationResult = _queryValidator.ThrowIfNull().Validate(query, model);

        // Assert
        validationResult.IsInvalid.Should().BeFalse();
    }

    [Test]
    public void Validation_should_fail_on_alias_in_filter_defined_in_same_node()
    {
        // Arrange
        var model = new Metamodel();
        const string queryString = "(t:test{prop=t.prop})";
        var queryAst = _queryParser.ThrowIfNull().Parse(queryString).As<QueryExpression>();
        var query = _queryBuilder.ThrowIfNull().FromAst(queryAst);

        // Act
        var validationResult = _queryValidator.ThrowIfNull().Validate(query, model);

        // Assert
        validationResult.IsInvalid.Should().BeFalse();
    }

    [Test]
    public void Validation_should_no_fail_on_defined_alias_in_filter()
    {
        // Arrange
        var model = new Metamodel();
        const string queryString = "(t:test)-[role]->(other{prop=t.prop})";
        var queryAst = _queryParser.ThrowIfNull().Parse(queryString).As<QueryExpression>();
        var query = _queryBuilder.ThrowIfNull().FromAst(queryAst);

        // Act
        var validationResult = _queryValidator.ThrowIfNull().Validate(query, model);

        // Assert
        validationResult.IsInvalid.Should().BeFalse();
    }
}