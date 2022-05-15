using NotionGraphDatabase.QueryEngine.Model;
using NUnit.Framework;

namespace NotionGraphDatabase.Test.QueryParsing;

internal class BasicQueryStatementsAreSupported : QueryParsingTestBase
{
    [Test]
    public void Select_all_attributes_from_node_type_is_parsed_correctly()
    {
        // Arrange
        const string queryString = "(test) return test.*";

        // Act
        var result = queryEngine.Parse(queryString);

        // Assert
        Assert.IsInstanceOf<QueryAbstractSyntaxTree>(result);
        var query = (QueryAbstractSyntaxTree) result;

        var selectExpression = query.SelectExpression;
        Assert.IsInstanceOf<NodeClassReference>(selectExpression);
        var nodeClassReference = (NodeClassReference) selectExpression;
        Assert.AreEqual(nodeClassReference.NodeIdentifier.Name, "test");
        Assert.AreSame(nodeClassReference.NodeIdentifier, nodeClassReference.Alias);

        Assert.AreEqual(query.ReturnSpecification.Selector.NodeTypeIdentifier.Name, "test");
        Assert.IsTrue(query.ReturnSpecification.Selector is SelectAllProperties);
    }
}