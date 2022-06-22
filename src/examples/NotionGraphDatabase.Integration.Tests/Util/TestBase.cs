using System;
using Microsoft.Extensions.DependencyInjection;
using NotionGraphDatabase.Interface;
using NUnit.Framework;
using Util.Extensions;

namespace NotionGraphDatabase.Integration.Tests.Util;

[TestFixture]
public abstract class TestBase
{
    private IServiceProvider? _serviceProvider;
    protected IGraphDatabase? NotionDatabase;
    protected ProxyRestClient? _proxyRestClient;
    protected ProxyNotionClient? _proxyNotionClient;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        _serviceProvider = DependencyInjectionSetup.CreateServiceProvider();
    }

    [SetUp]
    public void Setup()
    {
        NotionDatabase = _serviceProvider.ThrowIfNull().GetService<IGraphDatabase>().ThrowIfNull();
        _proxyRestClient = ProxyRestClient.LastCreated;
        _proxyNotionClient = ProxyNotionClient.LastCreated;
    }
}