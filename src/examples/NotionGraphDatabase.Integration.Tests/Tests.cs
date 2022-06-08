using System;
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
        result.Result.Rows.Should().HaveCount(3);
    }
}