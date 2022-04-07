using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using NotionGraphApi.Interface;
using NotionGraphDatabase.Interface;

namespace NotionGraphApi.Controllers;

[ApiController]
[Route("[controller]")]
public class QueryController : ControllerBase
{
    private readonly IGraphDatabase _database;
    private readonly ILogger<QueryController> _logger;


    public QueryController(IGraphDatabase database, ILogger<QueryController> logger)
    {
        _database = database;
        _logger = logger;
    }

    [HttpPost(Name = "execute")]
    public QueryResult Execute([FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Disallow)] Query query)
    {
        return new QueryResult();
    }
}