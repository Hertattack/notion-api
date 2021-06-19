using FluentAssertions;
using NUnit.Framework;

namespace Util.Test
{
    [TestFixture]
    public class HasOptionalParameterSupport
    {
        [Test]
        public void Option_can_be_none()
        {
            // Arrange
            Option<string> option;

            // Act
            option = Option.None;

            // Assert
            option.HasValue.Should().BeFalse();
        }

        [Test]
        public void Option_can_have_a_value()
        {
            // Arrange
            Option<string> option;

            // Act
            option = "Hi";

            // Assert
            option.HasValue.Should().BeTrue();
            option.Value.Should().Be("Hi");
        }

        [Test]
        public void Null_can_be_assigned_to_option_and_it_should_not_have_a_value_then()
        {
            // Arrange
            Option<string> option;

            // Act
            option = null;

            // Assert
            option.HasValue.Should().BeFalse();
        }
    }
}