﻿using System.Linq;
using FluentAssertions;
using NotionGraphDatabase.QueryEngine.Ast;
using NotionGraphDatabase.QueryEngine.Query.Expression;
using NotionGraphDatabase.QueryEngine.Query.Path;
using NUnit.Framework;

namespace NotionGraphDatabase.Test.QueryInterpretation;

internal class SelectionPathInterpretationIsSupportedTests : QueryInterpretationTestBase
{
    [Test]
    public void Single_path_is_handled_correctly()
    {
        // Arrange
        const string queryString = "(fromNode)-[roleName]->(toNode)";
        var result = _queryParser.Parse(queryString).As<QueryExpression>();

        // Act
        var query = _queryBuilder.FromAst(result);

        // Assert
        var steps = query.SelectSteps.ToList();
        steps.Should().HaveCount(2);

        var selectStep = steps[0].Step.As<NodeSelectStep>();
        selectStep.Filter.Should().HaveCount(0);
        selectStep.AssociatedNode.NodeName.Should().Be("fromNode");
        selectStep.AssociatedNode.Alias.Should().Be("fromNode");

        selectStep = steps[1].Step.As<NodeSelectStep>();
        selectStep.Role.Should().Be("roleName");
        selectStep.Filter.Should().HaveCount(0);
        selectStep.AssociatedNode.NodeName.Should().Be("toNode");
        selectStep.AssociatedNode.Alias.Should().Be("toNode");
    }

    [Test]
    public void Single_path_with_filters_is_handled_correctly()
    {
        // Arrange
        const string queryString = "(fromNode{property=1})-[roleName]->(toNode{property='value'})";
        var result = _queryParser.Parse(queryString).As<QueryExpression>();

        // Act
        var query = _queryBuilder.FromAst(result);

        // Assert
        var steps = query.SelectSteps.ToList();
        steps.Should().HaveCount(2);

        var selectStep = steps[0].Step.As<NodeSelectStep>();
        selectStep.Filter.Should().HaveCount(1);
        selectStep.Filter.First().Expression.As<IntCompareExpression>().Matches(1).Should().BeTrue();

        selectStep = steps[1].Step.As<NodeSelectStep>();
        selectStep.Filter.Should().HaveCount(1);
        selectStep.Filter.First().Expression.As<StringCompareExpression>().Matches("value").Should().BeTrue();
    }

    [Test]
    public void Long_path_is_handled_correctly()
    {
        // Arrange
        const string queryString =
            "(fromNode)-[roleName]->(toNode)-[nextRole]->(nextNode)-[longerPath]->(moarNode)-[finalDestination]->(finalNode)";
        var result = _queryParser.Parse(queryString).As<QueryExpression>();

        // Act
        var query = _queryBuilder.FromAst(result);

        // Assert
        var steps = query.SelectSteps.ToList();
        steps.Should().HaveCount(5);

        var selectStep = steps[0].Step.As<NodeSelectStep>();
        selectStep.Filter.Should().HaveCount(0);
        selectStep.AssociatedNode.NodeName.Should().Be("fromNode");
        selectStep.AssociatedNode.Alias.Should().Be("fromNode");

        selectStep = steps[1].Step.As<NodeSelectStep>();
        selectStep.Role.Should().Be("roleName");
        selectStep.Filter.Should().HaveCount(0);
        selectStep.AssociatedNode.NodeName.Should().Be("toNode");
        selectStep.AssociatedNode.Alias.Should().Be("toNode");

        selectStep = steps[2].Step.As<NodeSelectStep>();
        selectStep.Role.Should().Be("nextRole");
        selectStep.Filter.Should().HaveCount(0);
        selectStep.AssociatedNode.NodeName.Should().Be("nextNode");
        selectStep.AssociatedNode.Alias.Should().Be("nextNode");

        selectStep = steps[3].Step.As<NodeSelectStep>();
        selectStep.Role.Should().Be("longerPath");
        selectStep.Filter.Should().HaveCount(0);
        selectStep.AssociatedNode.NodeName.Should().Be("moarNode");
        selectStep.AssociatedNode.Alias.Should().Be("moarNode");

        selectStep = steps[4].Step.As<NodeSelectStep>();
        selectStep.Role.Should().Be("finalDestination");
        selectStep.Filter.Should().HaveCount(0);
        selectStep.AssociatedNode.NodeName.Should().Be("finalNode");
        selectStep.AssociatedNode.Alias.Should().Be("finalNode");
    }

    [Test]
    public void Long_path_with_aliases_is_handled_correctly()
    {
        // Arrange
        const string queryString =
            "(first:fromNode)-[roleName]->(t:toNode)-[nextRole]->(n:nextNode)-[longerPath]->(m:moarNode)-[finalDestination]->(f:finalNode)";
        var result = _queryParser.Parse(queryString).As<QueryExpression>();

        // Act
        var query = _queryBuilder.FromAst(result);

        // Assert
        var steps = query.SelectSteps.ToList();
        steps.Should().HaveCount(5);

        var selectStep = steps[0].Step.As<NodeSelectStep>();
        selectStep.AssociatedNode.NodeName.Should().Be("fromNode");
        selectStep.AssociatedNode.Alias.Should().Be("first");

        selectStep = steps[1].Step.As<NodeSelectStep>();
        selectStep.AssociatedNode.NodeName.Should().Be("toNode");
        selectStep.AssociatedNode.Alias.Should().Be("t");

        selectStep = steps[2].Step.As<NodeSelectStep>();
        selectStep.AssociatedNode.NodeName.Should().Be("nextNode");
        selectStep.AssociatedNode.Alias.Should().Be("n");

        selectStep = steps[3].Step.As<NodeSelectStep>();
        selectStep.AssociatedNode.NodeName.Should().Be("moarNode");
        selectStep.AssociatedNode.Alias.Should().Be("m");

        selectStep = steps[4].Step.As<NodeSelectStep>();
        selectStep.AssociatedNode.NodeName.Should().Be("finalNode");
        selectStep.AssociatedNode.Alias.Should().Be("f");
    }
}