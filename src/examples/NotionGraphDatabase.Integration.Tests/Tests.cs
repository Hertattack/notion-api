using System;
using System.Linq;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NotionGraphDatabase.Integration.Tests.Util;
using NotionGraphDatabase.Interface;
using NUnit.Framework;
using Util.Extensions;

namespace NotionGraphDatabase.Integration.Tests;

public class Tests
{
    private IServiceProvider serviceProvider;
    private IGraphDatabase notionDatabase;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        serviceProvider = DependencyInjectionSetup.CreateServiceProvider();
    }

    [SetUp]
    public void Setup()
    {
        notionDatabase = serviceProvider.GetService<IGraphDatabase>().ThrowIfNull();
    }

    [Test]
    public void Select_all_properties_from_single_database()
    {
        // Act
        var result = notionDatabase.Execute("(source)");

        // Assert
        result.ResultSet.Rows.Should().HaveCount(3);
    }

    [Test]
    public void Select_specific_property_from_single_database()
    {
        // Act
        var result = notionDatabase.Execute("(source) return source.Name");

        // Assert
        result.ResultSet.Rows.Should().HaveCount(3);

        var firstRow = result.ResultSet.Rows.First(r => (string) r["Name"]! == "C");
        firstRow.PropertyNames.Should().HaveCount(1);
    }

    [Test]
    public void Select_specific_properties_from_single_database()
    {
        // Act
        var result = notionDatabase.Execute("(s:source) return s.Name, s.'Last Edited'");

        // Assert
        result.ResultSet.Rows.Should().HaveCount(3);

        var firstRow = result.ResultSet.Rows.First(r => (string) r["Name"]! == "C");
        firstRow.PropertyNames.Should().BeEquivalentTo("Name", "Last Edited");
    }

    [Test]
    public void Single_node_can_be_filtered()
    {
        // Act
        var result = notionDatabase.Execute("(source{source.Name = 'C'})");

        // Assert
        result.ResultSet.Rows.Should().HaveCount(1);
    }

    [Test]
    public void A_path_of_nodes_can_be_selected()
    {
        // Act
        var result = notionDatabase.Execute("(source)-[Target]->(target)");

        // Assert
        result.ResultSet.Rows.Should().HaveCount(3);
    }
}