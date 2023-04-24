using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using NotionGraphApi.Interface;
using NotionGraphApi.Mapping;
using NotionGraphDatabase.Interface;

namespace NotionGraphApi.Controllers;

[ApiController]
[Route("[controller]")]
public class QueryController : ControllerBase
{
    private readonly IGraphDatabase _database;
    private readonly ILogger<QueryController> _logger;
    private readonly ResultMapper _resultMapper;


    public QueryController(IGraphDatabase database, ILogger<QueryController> logger, ILoggerFactory factory)
    {
        _database = database;
        _logger = logger;
        _resultMapper = new ResultMapper(factory.CreateLogger<ResultMapper>());
    }

    [HttpPost(Name = "advanced_query")]
    public QueryResult AdvancedQuery([FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Disallow)] Query query)
    {
        var internalResult = _database.Execute(query.QueryText);
        return _resultMapper.Map(internalResult);
    }

    [HttpGet(Name = "query")]
    public QueryResult Query([FromQuery(Name = "query")] string query)
    {
        _logger.LogDebug("Processing query: {Query}", query);
        var internalResult = _database.Execute(query);
        var queryResult = _resultMapper.Map(internalResult);
        return queryResult;
    }

    [HttpPost(Name = "analyze")]
    public QueryPlan AnalyzeQuery([FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Disallow)] Query query)
    {
        var internalResult = _database.AnalyzeQuery(query.QueryText);
        return _resultMapper.Map(internalResult);
    }
}