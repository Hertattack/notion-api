using System;
using System.IO;
using FluentAssertions;
using NotionVisualizer.SigmaJs;
using NUnit.Framework;

namespace NotionVisualizer.Test
{
    [TestFixture]
    public class SigmaJsDeployerTests
    {
        private string TempOutputPath { get; set; }

        [SetUp]
        public void SetUp()
        {
            TempOutputPath = Path.GetTempFileName();

            if (File.Exists(TempOutputPath))
                File.Delete(TempOutputPath);
        }

        [TearDown]
        public void TearDown()
        {
            Directory.Delete(TempOutputPath, true);
        }

        [Test]
        public void Should_export_sigma_js_package_to_destination()
        {
            if (!File.Exists("Resources\\sigma.js-1.2.1-build.zip"))
            {
                Console.WriteLine("Please build Sigma yourself from the sources.");
                return;
            }

            // Arrange
            var deployer = new SigmaJsDeployer();

            // Act
            deployer.Deploy(TempOutputPath, "Resources\\sigma.js-1.2.1-build.zip");

            // Assert
            Directory.Exists(TempOutputPath).Should().BeTrue();
            Directory.Exists(Path.Join(TempOutputPath, "plugins")).Should().BeTrue();
            File.Exists(Path.Join(TempOutputPath, "sigma.min.js")).Should().BeTrue();
        }
    }
}