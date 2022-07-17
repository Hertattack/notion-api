using Microsoft.AspNetCore.Mvc;
using NotionGraphDatabase.Interface;
using NotionGraphDatabase.Metadata;

namespace NotionGraphApi.Controllers;

[ApiController]
[Route("[controller]")]
public class MetamodelController
{
    private readonly IMetamodelStore _metamodelStore;

    public MetamodelController(IMetamodelStore metamodelStore)
    {
        _metamodelStore = metamodelStore;
    }

    [HttpGet]
    public Metamodel GetMetamodel()
    {
        return _metamodelStore.Metamodel;
    }
}