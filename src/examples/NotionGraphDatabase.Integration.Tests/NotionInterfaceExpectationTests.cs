using NUnit.Framework;

namespace NotionGraphDatabase.Integration.Tests;

[TestFixture]
public class NotionInterfaceExpectationTests : TestBase
{
    [Test]
    public void Notion_interface_has_supported_date_format()
    {
        // Arrange
        var result = notionDatabase.Execute("(source)");

        // Act

        // Assert
    }
}