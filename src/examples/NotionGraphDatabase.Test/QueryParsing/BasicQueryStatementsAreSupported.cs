using FluentAssertions;
using NotionGraphDatabase.QueryEngine.Model;
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
        var result = queryEngine.Parse(queryString);

        // Assert
        var selectExpression = result.SelectExpression;
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
        var result = queryEngine.Parse(queryString);

        // Assert
        result.ReturnSpecification.Should().BeAssignableTo<ReturnAllSpecification>();
    }

    [Test]
    public void Select_all_attributes_from_node_type_is_parsed_correctly()
    {
        // Arrange
        const string queryString = "(test) return test.*";

        // Act
        var result = queryEngine.Parse(queryString);

        // Assert
        var nodeClassReference = result.SelectExpression.As<NodeClassReference>();

        result.ReturnSpecification.Selector.NodeTypeIdentifier.Should().BeEquivalentTo(nodeClassReference.NodeIdentifier);
        result.ReturnSpecification.Selector.Should().BeAssignableTo<SelectAllProperties>();
    }

    [Test]
    public void Selection_of_node_without_alias_is_supported()
    {
        // Arrange
        const string queryString = "(test)";

        // Act
        var result = queryEngine.Parse(queryString);

        // Assert
        var nodeClassReference = result.SelectExpression.As<NodeClassReference>();

        nodeClassReference.NodeIdentifier.Name.Should().Be("test");
        nodeClassReference.NodeIdentifier.Should().BeSameAs(nodeClassReference.Alias);
    }
}