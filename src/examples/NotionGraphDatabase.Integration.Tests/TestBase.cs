using System;
using Microsoft.Extensions.DependencyInjection;
using NotionGraphDatabase.Integration.Tests.Util;
using NotionGraphDatabase.Interface;
using NUnit.Framework;
using Util.Extensions;

namespace NotionGraphDatabase.Integration.Tests;

[TestFixture]
public abstract class TestBase
{
    private IServiceProvider? _serviceProvider;
    protected IGraphDatabase? NotionDatabase;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        _serviceProvider = DependencyInjectionSetup.CreateServiceProvider();
    }

    [SetUp]
    public void Setup()
    {
        NotionDatabase = _serviceProvider.ThrowIfNull().GetService<IGraphDatabase>().ThrowIfNull();
    }
}