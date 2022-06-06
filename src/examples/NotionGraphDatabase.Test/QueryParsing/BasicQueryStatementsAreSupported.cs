using FluentAssertions;
using NotionGraphDatabase.QueryEngine.Ast;
using NUnit.Framework;

namespace NotionGraphDatabase.Test.QueryParsing;

internal class BasicQueryStatementsAreSupported : QueryParsingTestBase
{
    [Test]
    public void Single_node_selection_path_is_supported()
    {
        // Arrange
        const string queryString = "(test)";

        // Act
        var result = _queryParser.Parse(queryString);

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
        var result = _queryParser.Parse(queryString);

        // Assert
        result.As<QueryExpression>().ReturnSpecification.Should().BeAssignableTo<ReturnAllSpecification>();
    }

    [Test]
    public void Select_all_attributes_from_node_type_is_parsed_correctly()
    {
        // Arrange
        const string queryString = "(test) return test.*";

        // Act
        var result = _queryParser.Parse(queryString);

        // Assert
        var queryExpression = result.As<QueryExpression>();
        var nodeClassReference = queryExpression.SelectExpression.As<NodeClassReference>();

        queryExpression.ReturnSpecification.Selector.NodeTypeIdentifier.Should()
            .BeEquivalentTo(nodeClassReference.NodeIdentifier);
        queryExpression.ReturnSpecification.Selector.Should().BeAssignableTo<SelectAllProperties>();
    }

    [Test]
    public void Selection_of_node_without_alias_is_supported()
    {
        // Arrange
        const string queryString = "(test)";

        // Act
        var result = _queryParser.Parse(queryString);

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
        var result = _queryParser.Parse(queryString);

        // Assert
        var nodeClassReference = result.As<QueryExpression>().SelectExpression.As<NodeClassReference>();

        nodeClassReference.NodeIdentifier.Name.Should().Be("test");
        nodeClassReference.Alias.Name.Should().Be("t");
    }

    [Test]
    public void Selection_of_specific_return_properties_is_supported()
    {
        // Arrange
        const string queryString = "(test) return test.property_a";

        // Act
        var result = _queryParser.Parse(queryString);

        // Assert
        var returnSpecification = result.As<QueryExpression>().ReturnSpecification;
    }
}