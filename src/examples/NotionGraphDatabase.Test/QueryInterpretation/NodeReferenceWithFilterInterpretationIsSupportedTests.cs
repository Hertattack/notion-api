﻿using System.Linq;
using FluentAssertions;
using NotionGraphDatabase.Query.Expression;
using NotionGraphDatabase.Query.Parser.Ast;
using NotionGraphDatabase.Test.Util;
using NUnit.Framework;
using Util.Extensions;
using PropertyIdentifier = NotionGraphDatabase.Query.Expression.PropertyIdentifier;

namespace NotionGraphDatabase.Test.QueryInterpretation;

internal class NodeReferenceWithFilterInterpretationIsSupportedTests : QueryInterpretationTestBase
{
    [Test]
    public void Single_node_reference_with_single_property_string_value_filter_is_supported()
    {
        // Arrange
        const string queryString = "(test{property='value'})";
        var queryAst = _queryParser.ThrowIfNull().Parse(queryString).As<QueryExpression>();

        // Act
        var query = _queryBuilder.ThrowIfNull().FromAst(queryAst);

        // Assert
        var stepContexts = query.SelectSteps.ToList();
        stepContexts.Should().HaveCount(1);

        var filter = stepContexts.First().Step.Filter.ToList();
        filter.Should().HaveCount(1);
        filter[0].PropertyName.Should().Be("property");

        var expressionFunction = filter[0].Expression.As<StringExpression>();
        expressionFunction.Value.Should().Be("value");
    }

    [Test]
    public void Single_node_reference_with_single_property_int_value_filter_is_supported()
    {
        // Arrange
        const string queryString = "(test{property=1})";
        var queryAst = _queryParser.ThrowIfNull().Parse(queryString).As<QueryExpression>();

        // Act
        var query = _queryBuilder.ThrowIfNull().FromAst(queryAst);

        // Assert
        var stepContexts = query.SelectSteps.ToList();
        stepContexts.Should().HaveCount(1);

        var filter = stepContexts.First().Step.Filter.ToList();
        filter.Should().HaveCount(1);
        filter[0].PropertyName.Should().Be("property");

        var expressionFunction = filter[0].Expression.As<IntegerExpression>();
        expressionFunction.Value.Should().Be(1);
    }

    [Test]
    public void Single_node_reference_with_single_property_property_value_filter_is_supported()
    {
        // Arrange
        const string queryString = "(test{property=o.property2})";
        var queryAst = _queryParser.ThrowIfNull().Parse(queryString).As<QueryExpression>();

        // Act
        var query = _queryBuilder.ThrowIfNull().FromAst(queryAst);

        // Assert
        var stepContexts = query.SelectSteps.ToList();
        stepContexts.Should().HaveCount(1);

        var filter = stepContexts.First().Step.Filter.ToList();
        filter.Should().HaveCount(1);
        filter[0].PropertyName.Should().Be("property");

        var expressionFunction = filter[0].Expression.As<PropertyIdentifier>();
        expressionFunction.Alias.Should().Be("o");
        expressionFunction.PropertyName.Should().Be("property2");
    }

    [Test]
    public void Aliased_node_reference_with_single_property_int_value_filter_is_supported()
    {
        // Arrange
        const string queryString = "(t:test{property=3421})";
        var queryAst = _queryParser.ThrowIfNull().Parse(queryString).As<QueryExpression>();

        // Act
        var query = _queryBuilder.ThrowIfNull().FromAst(queryAst);

        // Assert
        var stepContexts = query.SelectSteps.ToList();
        stepContexts.Should().HaveCount(1);
        var step = stepContexts.First();
        step.Step.AssociatedNode.Alias.Should().Be("t");
        step.Step.AssociatedNode.NodeName.Should().Be("test");

        var filter = stepContexts.First().Step.Filter.ToList();
        filter.Should().HaveCount(1);
        filter[0].PropertyName.Should().Be("property");

        var expressionFunction = filter[0].Expression.As<IntegerExpression>();
        expressionFunction.Value.Should().Be(3421);
    }

    [Test]
    public void Single_node_reference_with_two_same_filters_is_supported()
    {
        // Arrange
        const string queryString = "(test{property=1, otherproperty=2})";
        var queryAst = _queryParser.ThrowIfNull().Parse(queryString).As<QueryExpression>();

        // Act
        var query = _queryBuilder.ThrowIfNull().FromAst(queryAst);

        // Assert
        var stepContexts = query.SelectSteps.ToList();
        stepContexts.Should().HaveCount(1);

        var filter = stepContexts.First().Step.Filter.ToList();
        filter.Should().HaveCount(2);

        filter[0].PropertyName.Should().Be("property");
        var expressionFunction = filter[0].Expression.As<IntegerExpression>();
        expressionFunction.Value.Should().Be(1);

        filter[1].PropertyName.Should().Be("otherproperty");
        expressionFunction = filter[1].Expression.As<IntegerExpression>();
        expressionFunction.Value.Should().Be(2);
    }

    [Test]
    public void Single_node_reference_with_two_different_filters_is_supported()
    {
        // Arrange
        const string queryString = "(test{property=1, otherproperty='str value'})";
        var queryAst = _queryParser.ThrowIfNull().Parse(queryString).As<QueryExpression>();

        // Act
        var query = _queryBuilder.ThrowIfNull().FromAst(queryAst);

        // Assert
        var stepContexts = query.SelectSteps.ToList();
        stepContexts.Should().HaveCount(1);

        var filter = stepContexts.First().Step.Filter.ToList();
        filter.Should().HaveCount(2);

        filter[0].PropertyName.Should().Be("property");
        var firstExpressionFunction = filter[0].Expression.As<IntegerExpression>();
        firstExpressionFunction.Value.Should().Be(1);

        filter[1].PropertyName.Should().Be("otherproperty");
        var secondExpressionFunction = filter[1].Expression.As<StringExpression>();
        secondExpressionFunction.Value.Should().Be("str value");
    }
}