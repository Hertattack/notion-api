// ReSharper disable once RedundantUsingDirective

using FluentAssertions;
using FluentAssertions.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NotionGraphDatabase.Integration.Tests.Util;
using NUnit.Framework;

namespace NotionGraphDatabase.Integration.Tests.Filtering;

public class StringFilterSupportTests : TestBase
{
    [Test]
    public void String_equals_filter_is_pushed_down()
    {
        // Arrange
        _proxyRestClient!.ExecuteRequests = r => r.Body is null;
        var expected = JsonResourceProvider.Get("filter-string-equals");

        // Act
        NotionDatabase!
            .Invoking(d => d.Execute("(s:source{s.FilterTestProperty='a'})"))
            .Should()
            .Throw<ProxyException>();

        // Assert
        var obj = (JToken) JsonConvert.DeserializeObject((string) _proxyRestClient.LastRequest!.Body!)!;
        obj.Should().BeEquivalentTo(expected);
    }

    [Test]
    public void String_contains_filter_is_pushed_down()
    {
        // Arrange
        _proxyRestClient!.ExecuteRequests = r => r.Body is null;
        var expected = JsonResourceProvider.Get("filter-string-contains");

        // Act
        NotionDatabase!
            .Invoking(d => d.Execute("(s:source{s.FilterTestProperty~='a'})"))
            .Should()
            .Throw<ProxyException>();

        // Assert
        var obj = (JToken) JsonConvert.DeserializeObject((string) _proxyRestClient.LastRequest!.Body!)!;
        obj.Should().BeEquivalentTo(expected);
    }
}