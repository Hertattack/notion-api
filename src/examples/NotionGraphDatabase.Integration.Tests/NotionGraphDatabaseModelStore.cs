using System;
using Microsoft.Extensions.Options;
using NotionGraphDatabase.Interface;
using NotionGraphDatabase.Metadata;

namespace NotionGraphDatabase.Integration.Tests;

public class NotionGraphDatabaseModelStore : IMetamodelStore
{
    public Metamodel Metamodel { get; }

    public NotionGraphDatabaseModelStore(IOptions<Metamodel> metamodelOption)
    {
        var metamodel = metamodelOption.Value;
        Metamodel = metamodel ?? throw new Exception($"Missing configuration for {nameof(NotionGraphApi)}");
    }
}