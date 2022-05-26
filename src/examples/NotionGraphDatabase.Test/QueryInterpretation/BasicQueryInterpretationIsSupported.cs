using System.Linq;
using FluentAssertions;
using NotionGraphDatabase.QueryEngine.Model;
using NotionGraphDatabase.QueryEngine.Query;
using NUnit.Framework;

namespace NotionGraphDatabase.Test.QueryInterpretation;

internal class BasicQueryInterpretationIsSupported : QueryInterpretationTestBase
{
    [Test]
    public void Single_consistent_select_and_return_query_is_interpreted_correctly()
    {
        // Arrange
        var queryAst = _queryParser.Parse("(test) return test.*").As<QueryExpression>();

        // Act
        var query = _queryBuilder.FromAst(queryAst);

        // Assert
        var selectedProperties = query.ReturnPropertySelections.ToList();
        selectedProperties.Count.Should().Be(1);
        var selection = selectedProperties[0];
        selection.NodeReference.Alias.Should().Be("test");
        selection.NodeReference.NodeName.Should().Be("test");

        selection.SelectedProperties.Count.Should().Be(1);
        var selectedProperty = selection.SelectedProperties[0];
        selectedProperty.Should().BeAssignableTo<NodeAllPropertiesSelected>();
        selectedProperty.ReferencedNode.Should().BeEquivalentTo(selection.NodeReference);
    }

    [Test]
    public void Node_selection_can_have_an_alias()
    {
        // Arrange
        var queryAst = _queryParser.Parse("(t:test)").As<QueryExpression>();

        // Act
        var query = _queryBuilder.FromAst(queryAst);

        // Assert
        query.NodeReferences.Should().Equal(new NodeReference("test", "t"));
    }

    [Test]
    public void Selecting_a_node_without_return_returns_all_properties()
    {
        // Arrange
        var queryAst = _queryParser.Parse("(t:test)").As<QueryExpression>();

        // Act
        var query = _queryBuilder.FromAst(queryAst);

        // Assert
        query.ReturnPropertySelections.Should().HaveCount(1);
        var returnSelection = query.ReturnPropertySelections.First().As<NodeReturnPropertySelection>();
        returnSelection.NodeReference.NodeName.Should().Be("test");
        returnSelection.SelectedProperties.Should().HaveCount(1)
            .And.Subject.First().Should().BeAssignableTo<NodeAllPropertiesSelected>();
    }
}