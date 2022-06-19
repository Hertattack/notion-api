using System.Linq;
using FluentAssertions;
using NotionGraphDatabase.QueryEngine.Ast;
using NotionGraphDatabase.QueryEngine.Query;
using NUnit.Framework;
using Util.Extensions;

namespace NotionGraphDatabase.Test.QueryInterpretation;

internal class BasicQueryInterpretationIsSupportedTests : QueryInterpretationTestBase
{
    [Test]
    public void Single_consistent_select_and_return_query_is_interpreted_correctly()
    {
        // Arrange
        var queryAst = _queryParser.ThrowIfNull().Parse("(test) return test.*").As<QueryExpression>();

        // Act
        var query = _queryBuilder.ThrowIfNull().FromAst(queryAst);

        // Assert
        var selectedProperties = query.ReturnPropertySelections.ToList();
        selectedProperties.Count.Should().Be(1);
        var selection = selectedProperties[0];
        selection.NodeReference.Alias.Should().Be("test");
        selection.NodeReference.NodeName.Should().Be("test");
        selection.PropertySelection.As<NodeAllPropertiesSelected>()
            .ReferencedNode.Should().BeEquivalentTo(selection.NodeReference);
    }

    [Test]
    public void Node_selection_can_have_an_alias()
    {
        // Arrange
        var queryAst = _queryParser.ThrowIfNull().Parse("(t:test)").As<QueryExpression>();

        // Act
        var query = _queryBuilder.ThrowIfNull().FromAst(queryAst);

        // Assert
        query.NodeReferences.Should().Equal(new NodeReference("test", "t"));
    }

    [Test]
    public void Selecting_a_node_without_return_returns_all_properties()
    {
        // Arrange
        var queryAst = _queryParser.ThrowIfNull().Parse("(t:test)").As<QueryExpression>();

        // Act
        var query = _queryBuilder.ThrowIfNull().FromAst(queryAst);

        // Assert
        query.ReturnPropertySelections.Should().HaveCount(1);
        var returnSelection = query.ReturnPropertySelections.First().As<NodeReturnPropertySelection>();
        returnSelection.NodeReference.NodeName.Should().Be("test");
        returnSelection.PropertySelection.Should().BeAssignableTo<NodeAllPropertiesSelected>();
    }

    [Test]
    public void Selecting_multiple_return_properties_is_supported()
    {
        // Arrange
        var queryAst = _queryParser.ThrowIfNull().Parse("(t:test) return t.A, t.'b B', t.c").As<QueryExpression>();

        // Act
        var query = _queryBuilder.ThrowIfNull().FromAst(queryAst);

        // Assert
        query.ReturnPropertySelections.Should().HaveCount(1);
        var returnSelection = query.ReturnPropertySelections.First().As<NodeReturnPropertySelection>();
        returnSelection.NodeReference.NodeName.Should().Be("test");
        var selection = returnSelection.PropertySelection.As<NodeSpecificPropertiesSelected>();
        selection.PropertyNames.Should().BeEquivalentTo("A", "b B", "c");
    }
}