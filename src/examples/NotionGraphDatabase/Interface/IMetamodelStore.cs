using NotionGraphDatabase.Metadata;

namespace NotionGraphDatabase.Interface;

public interface IMetamodelStore
{
    Metamodel Metamodel { get; }
}