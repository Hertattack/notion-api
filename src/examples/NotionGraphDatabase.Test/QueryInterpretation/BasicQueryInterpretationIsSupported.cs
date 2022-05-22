using System.Linq;
using FluentAssertions;
using NotionGraphDatabase.Query;
using NUnit.Framework;

namespace NotionGraphDatabase.Test.QueryInterpretation;

internal class BasicQueryInterpretationIsSupported : QueryInterpretationTestBase
{
    [Test]
    public void Single_consistent_select_and_return_query_is_interpreted_correctly()
    {
        // Arrange
        var queryAst = queryEngine.Parse("(test) return test.*");

        // Act
        var query = _queryBuilder.FromAst(queryAst);
        var selectedProperties = query.ReturnPropertySelections.ToList();

        // Assert
        selectedProperties.Count.Should().Be(1);
        var selection = selectedProperties[0];
        selection.NodeReference.Alias.Should().Be("test");
        selection.NodeReference.NodeName.Should().Be("test");

        selection.SelectedProperties.Count.Should().Be(1);
        var selectedProperty = selection.SelectedProperties[0];
        selectedProperty.Should().BeAssignableTo<NodeAllPropertiesSelected>();
        selectedProperty.ReferencedNode.Should().BeSameAs(selection.NodeReference);
    }
}