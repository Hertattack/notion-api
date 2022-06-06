using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NotionGraphDatabase.QueryEngine.Ast;
using NUnit.Framework;

namespace NotionGraphDatabase.Test.QueryParsing;

internal class SelectionPathsCanBeParsedTests : QueryParsingTestBase
{
    [Test]
    public void Single_path_is_handled_correctly()
    {
        // Arrange
        const string queryString = "(fromNode)-[roleName]->(toNode)";

        // Act
        var result = _queryParser.Parse(queryString);

        // Assert
        var selectExpression = result.As<QueryExpression>().SelectExpression.As<SelectPathExpression>();

        var fromExpression = selectExpression.FromExpression.As<NodeClassReference>();
        fromExpression.NodeIdentifier.Name.Should().Be("fromNode");

        selectExpression.ViaRole.Name.Should().Be("roleName");

        var toExpression = selectExpression.ToExpression.As<NodeClassReference>();
        toExpression.NodeIdentifier.Name.Should().Be("toNode");
    }

    [Test]
    public void Long_path_is_handled_correctly()
    {
        // Arrange
        const string queryString =
            "(fromNode)-[roleName]->(toNode)-[nextRole]->(nextNode)-[longerPath]->(moarNode)-[finalDestination]->(finalNode)";

        // Act
        var result = _queryParser.Parse(queryString);

        // Assert
        var selectExpression = result.As<QueryExpression>().SelectExpression.As<SelectPathExpression>();
        var steps = new List<NodeClassReference> {selectExpression.FromExpression.As<NodeClassReference>()};
        var roles = new List<Identifier> {selectExpression.ViaRole};

        while (selectExpression.ToExpression is SelectPathExpression toExpression)
        {
            steps.Add(toExpression.FromExpression.As<NodeClassReference>());
            roles.Add(toExpression.ViaRole);

            selectExpression = toExpression;
        }

        steps.Add(selectExpression.ToExpression.As<NodeClassReference>());

        steps.Select(s => s.NodeIdentifier.Name).Should()
            .BeEquivalentTo("fromNode", "toNode", "nextNode", "moarNode", "finalNode");
        roles.Select(r => r.Name).Should()
            .BeEquivalentTo("roleName", "nextRole", "longerPath", "finalDestination");
    }
}