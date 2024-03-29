﻿using System.Linq;
using FluentAssertions;
using NotionGraphDatabase.Query.Parser.Ast;
using NUnit.Framework;
using Util.Extensions;

namespace NotionGraphDatabase.Test.QueryParsing;

internal class NodeReferenceWithFilterIsSupportedTests : QueryParsingTestBase
{
    [Test]
    public void Single_node_reference_with_single_property_string_value_filter_is_supported()
    {
        // Arrange
        const string queryString = "(test{property='value'})";

        // Act
        var result = _queryParser.ThrowIfNull().Parse(queryString);

        // Assert
        var reference = result.As<QueryExpression>().SelectExpression.As<NodeClassReference>();
        reference.Alias.Name.Should().Be("test");
        reference.NodeIdentifier.Name.Should().Be("test");

        var filter = reference.Filter.Expressions.ToList();
        filter.Should().HaveCount(1);

        filter[0].PropertyName.Name.Should().Be("property");
        filter[0].Expression.As<StringValue>().Value.Should().Be("value");
        filter[0].Operator.Type.Should().Be(OperatorType.EQUALS);
    }

    [Test]
    public void Filter_with_negation_is_supported()
    {
        // Arrange
        const string queryString = "(test{property!='value'})";

        // Act
        var result = _queryParser.ThrowIfNull().Parse(queryString);

        // Assert
        var reference = result.As<QueryExpression>().SelectExpression.As<NodeClassReference>();
        reference.Alias.Name.Should().Be("test");
        reference.NodeIdentifier.Name.Should().Be("test");

        var filter = reference.Filter.Expressions.ToList();
        filter.Should().HaveCount(1);

        filter[0].PropertyName.Name.Should().Be("property");
        filter[0].Expression.As<StringValue>().Value.Should().Be("value");
        filter[0].Operator.IsNegated.Should().BeTrue();
        filter[0].Operator.Type.Should().Be(OperatorType.EQUALS);
    }

    [Test]
    public void Filter_with_contains_operator_is_supported()
    {
        // Arrange
        const string queryString = "(test{property~='value'})";

        // Act
        var result = _queryParser.ThrowIfNull().Parse(queryString);

        // Assert
        var reference = result.As<QueryExpression>().SelectExpression.As<NodeClassReference>();
        reference.Alias.Name.Should().Be("test");
        reference.NodeIdentifier.Name.Should().Be("test");

        var filter = reference.Filter.Expressions.ToList();
        filter.Should().HaveCount(1);

        filter[0].PropertyName.Name.Should().Be("property");
        filter[0].Expression.As<StringValue>().Value.Should().Be("value");
        filter[0].Operator.IsNegated.Should().BeFalse();
        filter[0].Operator.Type.Should().Be(OperatorType.CONTAINS);
    }

    [Test]
    public void Single_node_reference_with_single_property_int_value_filter_is_supported()
    {
        // Arrange
        const string queryString = "(test{property=1})";

        // Act
        var result = _queryParser.ThrowIfNull().Parse(queryString);

        // Assert
        var reference = result.As<QueryExpression>().SelectExpression.As<NodeClassReference>();
        reference.Alias.Name.Should().Be("test");
        reference.NodeIdentifier.Name.Should().Be("test");

        var filter = reference.Filter.Expressions.ToList();
        filter.Should().HaveCount(1);

        filter[0].PropertyName.Name.Should().Be("property");
        filter[0].Expression.As<IntValue>().Value.Should().Be(1);
    }

    [Test]
    public void Single_node_reference_with_single_property_property_value_filter_is_supported()
    {
        // Arrange
        const string queryString = "(test{property=o.property2})";

        // Act
        var result = _queryParser.ThrowIfNull().Parse(queryString);

        // Assert
        var reference = result.As<QueryExpression>().SelectExpression.As<NodeClassReference>();
        reference.Alias.Name.Should().Be("test");
        reference.NodeIdentifier.Name.Should().Be("test");

        var filter = reference.Filter.Expressions.ToList();
        filter.Should().HaveCount(1);

        filter[0].PropertyName.Name.Should().Be("property");
        filter[0].Expression.As<PropertyIdentifier>().NodeNameOrAlias.Name.Should().Be("o");
        filter[0].Expression.As<PropertyIdentifier>().PropertyName.Name.Should().Be("property2");
    }

    [Test]
    public void Aliased_node_reference_with_single_property_int_value_filter_is_supported()
    {
        // Arrange
        const string queryString = "(t:test{property=1})";

        // Act
        var result = _queryParser.ThrowIfNull().Parse(queryString);

        // Assert
        var reference = result.As<QueryExpression>().SelectExpression.As<NodeClassReference>();
        var filter = reference.Filter.Expressions.ToList();
        filter.Should().HaveCount(1);
        filter[0].PropertyName.Name.Should().Be("property");
        filter[0].Expression.As<IntValue>().Value.Should().Be(1);
        reference.Alias.Name.Should().Be("t");
        reference.NodeIdentifier.Name.Should().Be("test");
    }

    [Test]
    public void Single_node_reference_with_two_same_filters_is_supported()
    {
        // Arrange
        const string queryString = "(test{property=1, otherproperty=2})";

        // Act
        var result = _queryParser.ThrowIfNull().Parse(queryString);

        // Assert
        var reference = result.As<QueryExpression>().SelectExpression.As<NodeClassReference>();

        var filter = reference.Filter.Expressions.ToList();
        filter.Should().HaveCount(2);

        filter[0].PropertyName.Name.Should().Be("property");
        filter[0].Expression.As<IntValue>().Value.Should().Be(1);

        filter[1].PropertyName.Name.Should().Be("otherproperty");
        filter[1].Expression.As<IntValue>().Value.Should().Be(2);
    }

    [Test]
    public void Single_node_reference_with_two_different_filters_is_supported()
    {
        // Arrange
        const string queryString = "(test{property=1, otherproperty='str value'})";

        // Act
        var result = _queryParser.ThrowIfNull().Parse(queryString);

        // Assert
        var reference = result.As<QueryExpression>().SelectExpression.As<NodeClassReference>();

        var filter = reference.Filter.Expressions.ToList();
        filter.Should().HaveCount(2);

        filter[0].PropertyName.Name.Should().Be("property");
        filter[0].Expression.As<IntValue>().Value.Should().Be(1);

        filter[1].PropertyName.Name.Should().Be("otherproperty");
        filter[1].Expression.As<StringValue>().Value.Should().Be("str value");
    }
}