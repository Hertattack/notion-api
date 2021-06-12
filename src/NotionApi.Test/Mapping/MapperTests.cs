using FluentAssertions;
using NotionApi.Request;
using NotionApi.Test.Mapping.Strategies.Fixtures;
using NUnit.Framework;

namespace NotionApi.Test.Mapping
{
    [TestFixture]
    public class MapperTests
    {
        protected Mapper mapper = new();

        class When_mapping_enumerations : MapperTests
        {
            [Test]
            public void Without_mapping_it_should_return_the_enumeration_value()
            {
                // Arrange
                var enumValue = MappingTestEnumeration.SomeValue;

                // Act
                var mappedValue = mapper.MapEnumeration(typeof(MappingTestEnumeration), enumValue);

                // Assert
                mappedValue.Should().Be("SomeValue");
            }

            [Test]
            public void With_mapping_it_should_return_the_name_from_the_mapping()
            {
                // Arrange
                var enumValue = MappingTestEnumeration.OtherValue;

                // Act
                var mappedValue = mapper.MapEnumeration(typeof(MappingTestEnumeration), enumValue);

                // Assert
                mappedValue.Should().Be("should-use-this");
            }
            
            [Test]
            public void With_mapping_and_strategy_it_should_return_the_value_using_the_strategy()
            {
                // Arrange
                var enumValue = MappingTestEnumeration.IShouldBecomeLowerCase;

                // Act
                var mappedValue = mapper.MapEnumeration(typeof(MappingTestEnumeration), enumValue);

                // Assert
                mappedValue.Should().Be("ishouldbecomelowercase");
            }
        }
    }
}