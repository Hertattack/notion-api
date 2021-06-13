using System.Collections.Generic;
using FluentAssertions;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using NotionApi.Request;
using NotionApi.Request.Mapping;
using NSubstitute;
using NUnit.Framework;

namespace NotionApi.Test.Mapping.Strategies
{
    [TestFixture]
    public class ToJsonDocumentStrategyTests
    {
        protected IMapper mapper;
        protected ToJsonDocumentStrategy jsonDocumentStrategy;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            mapper = Substitute.For<IMapper>();
            jsonDocumentStrategy = new ToJsonDocumentStrategy(mapper);
        }

        [Test]
        public void A_dictionary_is_translated_to_a_json_document()
        {
            // Arrange
            var input = new Dictionary<string, object> {{"a", 1}, {"b", true}, {"c", "value"}, {"d", new Dictionary<string, object> {{"e", "eeee"}}}};

            // Act
            var result = jsonDocumentStrategy.GetValue(input.GetType(), input);

            // Assert
            result.HasValue.Should().BeTrue();

            var document = (string) result.Value;
            document.Should().Be(Resources.JsonDocument);
        }
    }
}