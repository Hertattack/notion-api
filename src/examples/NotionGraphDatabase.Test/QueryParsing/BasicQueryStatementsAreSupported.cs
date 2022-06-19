using System.Linq;
using FluentAssertions;
using NotionGraphDatabase.Query.Parser.Ast;
using NUnit.Framework;
using Util.Extensions;

namespace NotionGraphDatabase.Test.QueryParsing;

internal class BasicQueryStatementsAreSupported : QueryParsingTestBase
{
    [Test]
    public void Single_node_selection_path_is_supported()
    {
        // Arrange
        const string queryString = "(test)";

        // Act
        var result = _queryParser.ThrowIfNull().Parse(queryString);

        // Assert
        var selectExpression = result.As<QueryExpression>().SelectExpression;
        Assert.IsInstanceOf<NodeClassReference>(selectExpression);

        var nodeClassReference = (NodeClassReference) selectExpression;
        Assert.AreEqual(nodeClassReference.NodeIdentifier.Name, "test");
        Assert.AreSame(nodeClassReference.NodeIdentifier, nodeClassReference.Alias);
    }

    [Test]
    public void No_return_specification_is_supported()
    {
        // Arrange
        const string queryString = "(test)";

        // Act
        var result = _queryParser.ThrowIfNull().Parse(queryString);

        // Assert
        result.As<QueryExpression>().ReturnSpecification.Should().BeAssignableTo<ReturnAllSpecification>();
    }

    [Test]
    public void Select_all_attributes_from_node_type_is_parsed_correctly()
    {
        // Arrange
        const string queryString = "(test) return test.*";

        // Act
        var result = _queryParser.ThrowIfNull().Parse(queryString);

        // Assert
        var queryExpression = result.As<QueryExpression>();
        var nodeClassReference = queryExpression.SelectExpression.As<NodeClassReference>();

        queryExpression.ReturnSpecification.Selectors.Should().HaveCount(1);

        var selector = queryExpression.ReturnSpecification.Selectors.First();
        selector.NodeIdentifier.Should().BeEquivalentTo(nodeClassReference.NodeIdentifier);
        selector.Should().BeAssignableTo<SelectAllProperties>();
    }

    [Test]
    public void Selection_of_node_without_alias_is_supported()
    {
        // Arrange
        const string queryString = "(test)";

        // Act
        var result = _queryParser.ThrowIfNull().Parse(queryString);

        // Assert
        var nodeClassReference = result.As<QueryExpression>().SelectExpression.As<NodeClassReference>();

        nodeClassReference.NodeIdentifier.Name.Should().Be("test");
        nodeClassReference.NodeIdentifier.Should().BeSameAs(nodeClassReference.Alias);
    }

    [Test]
    public void Selection_of_node_with_alias_is_supported()
    {
        // Arrange
        const string queryString = "(t:test)";

        // Act
        var result = _queryParser.ThrowIfNull().Parse(queryString);

        // Assert
        var nodeClassReference = result.As<QueryExpression>().SelectExpression.As<NodeClassReference>();

        nodeClassReference.NodeIdentifier.Name.Should().Be("test");
        nodeClassReference.Alias.Name.Should().Be("t");
    }

    [Test]
    public void Selection_of_specific_return_property_is_supported()
    {
        // Arrange
        const string queryString = "(test) return test.property_a";

        // Act
        var result = _queryParser.ThrowIfNull().Parse(queryString);

        // Assert
        var returnSpecification = result.As<QueryExpression>().ReturnSpecification;
        returnSpecification.Selectors.Should().HaveCount(1);

        var selector = returnSpecification.Selectors.First().As<SelectSpecificProperty>();
        selector.NodeIdentifier.Name.Should().Be("test");
        selector.PropertyName.Should().Be("property_a");
    }

    [Test]
    public void Selection_of_specific_return_property_string_is_supported()
    {
        // Arrange
        const string queryString = "(test) return test.'Property A'";

        // Act
        var result = _queryParser.ThrowIfNull().Parse(queryString);

        // Assert
        var returnSpecification = result.As<QueryExpression>().ReturnSpecification;
        returnSpecification.Selectors.Should().HaveCount(1);

        var selector = returnSpecification.Selectors.First().As<SelectSpecificProperty>();
        selector.NodeIdentifier.Name.Should().Be("test");
        selector.PropertyName.Should().Be("Property A");
    }

    [Test]
    public void Multiple_selections_of_specific_return_properties_are_supported()
    {
        // Arrange
        const string queryString = "(test) return test.'Property A', test.property_b, test.'other prop'";

        // Act
        var result = _queryParser.ThrowIfNull().Parse(queryString);

        // Assert
        var returnSpecification = result.As<QueryExpression>().ReturnSpecification;
        returnSpecification.Selectors.Should().HaveCount(3);

        returnSpecification.Selectors.Should()
            .ContainSingle(i => i.As<SelectSpecificProperty>().PropertyName == "Property A")
            .And
            .ContainSingle(i => i.As<SelectSpecificProperty>().PropertyName == "property_b")
            .And
            .ContainSingle(i => i.As<SelectSpecificProperty>().PropertyName == "other prop");
    }
}