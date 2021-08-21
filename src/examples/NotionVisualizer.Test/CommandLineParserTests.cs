using System;
using System.Linq;
using FluentAssertions;
using NotionVisualizer.Util;
using NUnit.Framework;

namespace NotionVisualizer.Test
{
    [TestFixture]
    public class CommandLineParserTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Should_support_options_without_value()
        {
            // Arrange
            var noValueOption = new CommandLineOption
            {
                Name = "novalue",
                Description = "Element with no value",
                Required = true,
                HasValue = false
            };
            var parser = new CommandLineParser(noValueOption);

            // Act
            var values = parser.Parse(new[] { "--novalue" }).ToList();

            // Assert
            values.Should().ContainSingle(v => !v.HasValue && v.Option == noValueOption);

            values.Should().HaveCount(1);
        }

        [Test]
        public void Should_not_recognize_values_without_option()
        {
            // Arrange
            var noValueOption = new CommandLineOption
            {
                Name = "with-value",
                Description = "Element with value",
                Required = true,
                HasValue = false
            };
            var parser = new CommandLineParser(noValueOption);

            // Act
            Action action = () => parser.Parse(new[] { "novalue" });

            // Assert
            action.Should().Throw<ArgumentException>();
        }

        [Test]
        public void Should_recognize_an_option_with_value()
        {
            // Arrange
            var valueOption = new CommandLineOption
            {
                Name = "with-value",
                Description = "Element with value",
                Required = true
            };
            var parser = new CommandLineParser(valueOption);

            // Act
            var values = parser.Parse(new[] { "--with-value", "value" }).ToList();

            // Assert
            values.Should()
                .ContainSingle(v =>
                    v.HasValue
                    && v.Option == valueOption
                    && v.Value == "value");

            values.Should().HaveCount(1);
        }

        [Test]
        public void Should_support_multiple_options()
        {
            // Arrange
            var valueOption = new CommandLineOption
            {
                Name = "with-value",
                Description = "Element with value",
                Required = true
            };
            var noValueOption = new CommandLineOption
            {
                Name = "novalue",
                Description = "Element with no value",
                Required = true,
                HasValue = false
            };
            var parser = new CommandLineParser(valueOption, noValueOption);

            // Act
            var values = parser.Parse(new[] { "--with-value", "value", "--novalue" }).ToList();

            // Assert
            values.Should()
                .ContainSingle(v =>
                    v.HasValue
                    && v.Option == valueOption
                    && v.Value == "value");

            values.Should().ContainSingle(v => !v.HasValue && v.Option == noValueOption);

            values.Should().HaveCount(2);
        }
    }
}