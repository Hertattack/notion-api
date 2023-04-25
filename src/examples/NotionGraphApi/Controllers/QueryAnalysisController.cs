using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using NotionGraphApi.Interface;
using NotionGraphApi.Interface.Analysis;
using NotionGraphApi.Mapping;
using NotionGraphDatabase.Interface;

namespace NotionGraphApi.Controllers;

[ApiController]
[Route("[controller]")]
public class QueryAnalysisController : ControllerBase
{
    private readonly IGraphDatabase _database;
    private readonly ILogger<QueryAnalysisController> _logger;
    private readonly QueryAnalysisMapper _analysisMapper;


    public QueryAnalysisController(IGraphDatabase database, ILogger<QueryAnalysisController> logger)
    {
        _database = database;
        _logger = logger;
        _analysisMapper = new QueryAnalysisMapper();
    }

    [HttpPost(Name = "analyze")]
    public QueryPlan AnalyzeQuery([FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Disallow)] Query query)
    {
        _logger.LogInformation("Query analysis request for query: {QueryText}", query.QueryText);
        var internalResult = _database.AnalyzeQuery(query.QueryText);

        _logger.LogInformation("Map query analysis result");
        var result = _analysisMapper.Map(internalResult);

        _logger.LogInformation("Query analysis finished");
        return result;
    }
}