using NotionGraphDatabase.Metadata;

namespace NotionGraphDatabase.Interface;

public interface IMetamodelFactory
{
    Model CreateModel();
}