using System.Linq;
using FluentAssertions;
using NotionGraphDatabase.QueryEngine.Model;
using NotionGraphDatabase.QueryEngine.Query.Expression;
using NUnit.Framework;

namespace NotionGraphDatabase.Test.QueryInterpretation;

internal class NodeReferenceWithFilterInterpretationIsSupportedTests : QueryInterpretationTestBase
{
    [Test]
    public void Single_node_reference_with_single_property_string_value_filter_is_supported()
    {
        // Arrange
        const string queryString = "(test{property='value'})";
        var queryAst = _queryParser.Parse(queryString).As<QueryExpression>();

        // Act
        var query = _queryBuilder.FromAst(queryAst);

        // Assert
        var stepContexts = query.SelectSteps.ToList();
        stepContexts.Should().HaveCount(1);

        var filter = stepContexts.First().Step.Filter.ToList();
        filter.Should().HaveCount(1);
        filter[0].PropertyName.Should().Be("property");

        var expressionFunction = filter[0].Expression.As<StringCompareExpression>();
        expressionFunction.Matches("value").Should().BeTrue();
    }

    [Test]
    public void Single_node_reference_with_single_property_int_value_filter_is_supported()
    {
        // Arrange
        const string queryString = "(test{property=1})";
        var queryAst = _queryParser.Parse(queryString).As<QueryExpression>();

        // Act
        var query = _queryBuilder.FromAst(queryAst);

        // Assert
        var stepContexts = query.SelectSteps.ToList();
        stepContexts.Should().HaveCount(1);

        var filter = stepContexts.First().Step.Filter.ToList();
        filter.Should().HaveCount(1);
        filter[0].PropertyName.Should().Be("property");

        var expressionFunction = filter[0].Expression.As<IntCompareExpression>();
        expressionFunction.Matches(1).Should().BeTrue();
    }

    [Test]
    public void Single_node_reference_with_single_property_property_value_filter_is_supported()
    {
        // Arrange
        const string queryString = "(test{property=o.property2})";
        var queryAst = _queryParser.Parse(queryString).As<QueryExpression>();

        // Act
        var query = _queryBuilder.FromAst(queryAst);

        // Assert
        var stepContexts = query.SelectSteps.ToList();
        stepContexts.Should().HaveCount(1);

        var filter = stepContexts.First().Step.Filter.ToList();
        filter.Should().HaveCount(1);
        filter[0].PropertyName.Should().Be("property");

        var expressionFunction = filter[0].Expression.As<PropertyValueCompareExpression>();
        expressionFunction.RightAlias.Should().Be("o");
        expressionFunction.RightPropertyName.Should().Be("property2");
    }

    [Test]
    public void Aliased_node_reference_with_single_property_int_value_filter_is_supported()
    {
        // Arrange
        const string queryString = "(t:test{property=3421})";
        var queryAst = _queryParser.Parse(queryString).As<QueryExpression>();

        // Act
        var query = _queryBuilder.FromAst(queryAst);

        // Assert
        var stepContexts = query.SelectSteps.ToList();
        stepContexts.Should().HaveCount(1);
        var step = stepContexts.First();
        step.Step.AssociatedNode.Alias.Should().Be("t");
        step.Step.AssociatedNode.NodeName.Should().Be("test");

        var filter = stepContexts.First().Step.Filter.ToList();
        filter.Should().HaveCount(1);
        filter[0].PropertyName.Should().Be("property");

        var expressionFunction = filter[0].Expression.As<IntCompareExpression>();
        expressionFunction.Matches(3421).Should().BeTrue();
    }

    [Test]
    public void Single_node_reference_with_two_same_filters_is_supported()
    {
        // Arrange
        const string queryString = "(test{property=1, otherproperty=2})";
        var queryAst = _queryParser.Parse(queryString).As<QueryExpression>();

        // Act
        var query = _queryBuilder.FromAst(queryAst);

        // Assert
        var stepContexts = query.SelectSteps.ToList();
        stepContexts.Should().HaveCount(1);

        var filter = stepContexts.First().Step.Filter.ToList();
        filter.Should().HaveCount(2);

        filter[0].PropertyName.Should().Be("property");
        var expressionFunction = filter[0].Expression.As<IntCompareExpression>();
        expressionFunction.Matches(1).Should().BeTrue();

        filter[1].PropertyName.Should().Be("otherproperty");
        expressionFunction = filter[1].Expression.As<IntCompareExpression>();
        expressionFunction.Matches(2).Should().BeTrue();
    }

    [Test]
    public void Single_node_reference_with_two_different_filters_is_supported()
    {
        // Arrange
        const string queryString = "(test{property=1, otherproperty='str value'})";
        var queryAst = _queryParser.Parse(queryString).As<QueryExpression>();

        // Act
        var query = _queryBuilder.FromAst(queryAst);

        // Assert
        var stepContexts = query.SelectSteps.ToList();
        stepContexts.Should().HaveCount(1);

        var filter = stepContexts.First().Step.Filter.ToList();
        filter.Should().HaveCount(2);

        filter[0].PropertyName.Should().Be("property");
        var firstExpressionFunction = filter[0].Expression.As<IntCompareExpression>();
        firstExpressionFunction.Matches(1).Should().BeTrue();

        filter[1].PropertyName.Should().Be("otherproperty");
        var secondExpressionFunction = filter[1].Expression.As<StringCompareExpression>();
        secondExpressionFunction.Matches("str value").Should().BeTrue();
    }
}