﻿using Newtonsoft.Json;
using NotionGraphApi.Interface;

namespace NotionGraphApi.Mapping;

public class ResultMapper
{
    private readonly ILogger<ResultMapper> _logger;

    public ResultMapper(ILogger<ResultMapper> logger)
    {
        _logger = logger;
    }

    public QueryResult Map(NotionGraphDatabase.Interface.Result.QueryResult internalResult)
    {
        var result = new QueryResult();

        if (!internalResult.ResultSet.Rows.Any())
            return result;

        result.PropertyNames = internalResult.ResultSet.Rows.First().PropertyNames.ToList();

        foreach (var row in internalResult.ResultSet.Rows)
        {
            var outputRow = new Row();
            foreach (var propertyName in result.PropertyNames)
                outputRow.AddFieldValue(propertyName.Alias, propertyName.Name, row[propertyName]);

            result.AddRow(outputRow);
        }

        return result;
    }
}