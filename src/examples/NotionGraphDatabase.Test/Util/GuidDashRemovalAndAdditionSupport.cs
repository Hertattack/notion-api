using FluentAssertions;
using NotionGraphDatabase.Util;
using NUnit.Framework;

namespace NotionGraphDatabase.Test.Util;

[TestFixture]
public class GuidDashRemovalAndAdditionSupport
{
    [Test]
    public void Should_support_adding_dashes_to_a_guid()
    {
        // arrange
        var noDashes = "c6a5c8ba7cd544c2893ca368ff6b4e6a";

        // act
        var hasDashes = noDashes.AddDashes();

        // assert
        hasDashes.Should().Be("c6a5c8ba-7cd5-44c2-893c-a368ff6b4e6a");
    }

    [Test]
    public void Should_support_removign_dashes_from_a_guid()
    {
        // arrange
        var hasDashes = "c6a5c8ba-7cd5-44c2-893c-a368ff6b4e6a";

        // act
        var noDashes = hasDashes.RemoveDashes();

        // assert
        noDashes.Should().Be("c6a5c8ba7cd544c2893ca368ff6b4e6a");
    }
}