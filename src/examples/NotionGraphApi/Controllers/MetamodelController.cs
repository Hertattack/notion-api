using Microsoft.AspNetCore.Mvc;
using NotionGraphDatabase.Interface;
using NotionGraphDatabase.Metadata;

namespace NotionGraphApi.Controllers;

[ApiController]
[Route("[controller]")]
public class MetamodelController : ControllerBase
{
    private readonly IMetamodelStore _metamodelStore;
    private readonly IGraphDatabase _graphDatabase;

    public MetamodelController(
        IMetamodelStore metamodelStore,
        IGraphDatabase graphDatabase)
    {
        _metamodelStore = metamodelStore;
        _graphDatabase = graphDatabase;
    }

    [HttpGet]
    public Metamodel GetMetamodel()
    {
        return _metamodelStore.Metamodel;
    }

    [HttpGet("databaseDefinition")]
    public IActionResult GetDatabaseDefinition(string alias)
    {
        var database = _metamodelStore.Metamodel.Databases.FirstOrDefault(d => d.Alias == alias);

        if (database is null)
            return NotFound($"Database with alias: {alias} could not be found in metamodel");

        return Ok(_graphDatabase.GetDatabaseDefinition(database.Id));
    }
}