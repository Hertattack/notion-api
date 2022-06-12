using System;
using Microsoft.Extensions.DependencyInjection;
using NotionGraphDatabase.Integration.Tests.Util;
using NotionGraphDatabase.Interface;
using NUnit.Framework;
using Util.Extensions;

namespace NotionGraphDatabase.Integration.Tests;

[TestFixture]
public class TestBase
{
    protected IServiceProvider serviceProvider;
    protected IGraphDatabase notionDatabase;

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
}