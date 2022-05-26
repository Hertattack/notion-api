using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using RestUtil.Request;
using RestUtil.Test.Fixtures;
using Util;

namespace RestUtil.Test.Mapping;

[TestFixture]
public class MapperTests
{
    private readonly Mapper _mapper = new();

    private class When_mapping_enumerations : MapperTests
    {
        [Test]
        public void Without_mapping_it_should_return_the_enumeration_value()
        {
            // Arrange
            var enumValue = MappingTestEnumeration.SomeValue;

            // Act
            var mappedValue = _mapper.MapEnumeration(typeof(MappingTestEnumeration), enumValue);

            // Assert
            mappedValue.Should().Be("SomeValue");
        }

        [Test]
        public void With_mapping_it_should_return_the_name_from_the_mapping()
        {
            // Arrange
            var enumValue = MappingTestEnumeration.OtherValue;

            // Act
            var mappedValue = _mapper.MapEnumeration(typeof(MappingTestEnumeration), enumValue);

            // Assert
            mappedValue.Should().Be("should-use-this");
        }

        [Test]
        public void With_mapping_and_strategy_it_should_return_the_value_using_the_strategy()
        {
            // Arrange
            var enumValue = MappingTestEnumeration.IShouldBecomeLowerCase;

            // Act
            var mappedValue = _mapper.MapEnumeration(typeof(MappingTestEnumeration), enumValue);

            // Assert
            mappedValue.Should().Be("ishouldbecomelowercase");
        }
    }

    private class When_mapping_a_non_nested_structure : MapperTests
    {
        [Test]
        public void The_structure_can_be_converted_to_a_dictionary()
        {
            // Arrange
            var basicStructure = new NonNestedBasicStructure() {First = false, Second = 123, Third = "Ok"};

            // Act
            var result = _mapper.Map(basicStructure);

            // Assert
            result.Should().BeAssignableTo<Dictionary<string, object>>();
            var dict = (Dictionary<string, object>) result;

            dict.Should().ContainKey("a");
            dict["a"].Should().Be(false);

            dict.Should().ContainKey("b");
            dict["b"].Should().Be(123);

            dict.Should().ContainKey("Third");
            dict["Third"].Should().Be("Ok");
        }

        [Test]
        public void The_structure_can_be_converted_to_a_dictionary_null_is_included()
        {
            // Arrange
            var basicStructure = new NonNestedBasicStructure() {Third = null};

            // Act
            var result = _mapper.Map(basicStructure);

            // Assert
            result.Should().BeAssignableTo<Dictionary<string, object>>();
            var dict = (Dictionary<string, object>) result;

            dict.Should().NotContainKey("Third");
        }
    }

    private class When_mapping_a_nested_structure : MapperTests
    {
        [Test]
        public void The_structure_should_be_represented_as_nested_dictionaries()
        {
            // Arrange
            var nested = new BasicNestedStructure
            {
                Nested = new BasicNestedStructure {SomeValue = "Val2", Nested = Option.None},
                SomeValue = "val1"
            };

            // Act
            var result = _mapper.Map(nested);

            // Assert
            result.Should().BeAssignableTo<Dictionary<string, object>>();
            result.Should().BeEquivalentTo(new Dictionary<string, object>
            {
                {"Nested", new Dictionary<string, object> {{"SomeValue", "Val2"}}},
                {"SomeValue", "val1"}
            });
        }
    }
}