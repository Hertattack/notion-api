using NUnit.Framework;
using Util.Extensions;

namespace NotionGraphDatabase.Integration.Tests;

[TestFixture]
public class NotionInterfaceExpectationTests : TestBase
{
    [Test]
    public void Notion_interface_has_supported_date_format()
    {
        // Arrange
        var result = NotionDatabase.ThrowIfNull().Execute("(source)");

        // Act

        // Assert
    }
}