using System.Linq;
using FluentAssertions;
using NotionGraphDatabase.Integration.Tests.Util;
using NUnit.Framework;

namespace NotionGraphDatabase.Integration.Tests.Selecting;

public class SelectingNodesMultipleTimes : TestBase
{
    [Test]
    public void It_is_supported_to_select_the_same_node_multiple_times()
    {
        // Act
        var result = NotionDatabase!.Execute("(source)-[Target]->(target)-[Source]->(s2:source)");

        // Assert
        result.ResultSet.Rows.ToList().Should().HaveCount(9);
    }
}