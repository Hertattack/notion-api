using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NotionVisualizer.Visualization;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace NotionVisualizer.Generator.Excel;

public class ExcelGenerator : BaseGenerator
{
    private readonly ILogger<ExcelGenerator> _logger;
    private readonly ExcelGeneratorOptions _options;
    private readonly IReadOnlyList<PropertyInfo> _nodeProperties;
    private readonly IReadOnlyList<PropertyInfo> _edgeProperties;

    public ExcelGenerator(
        IOptions<ExcelGeneratorOptions> options,
        ILogger<ExcelGenerator> logger)
    {
        _logger = logger;
        _options = options.Value;

        _nodeProperties = typeof(Node).GetProperties().Where(p => _options.NodeProperties.Contains(p.Name)).ToList();
        _edgeProperties = typeof(Edge).GetProperties().Where(p => _options.EdgeProperties.Contains(p.Name)).ToList();
    }

    public override void Generate(string basePath, Graph graph)
    {
        var outputPath = Path.Join(basePath, _options.Filename);
        _logger.LogInformation("Generating Excel output to {outputPath}", outputPath);

        using (var outputStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
        {
            var workbook = new XSSFWorkbook();
            AddSheet(workbook, "Nodes", graph.Nodes, _nodeProperties);
            AddSheet(workbook, "Edges", graph.Edges, _edgeProperties);

            workbook.Write(outputStream);
        }

        _logger.LogInformation("Excel generation finished");
    }

    private static void AddSheet(
        IWorkbook workbook,
        string sheetname,
        IEnumerable<object> sourceObjects,
        IReadOnlyList<PropertyInfo> properties)
    {
        var sheet = workbook.CreateSheet(sheetname);
        var rowIndex = 0;
        var currentRow = sheet.CreateRow(rowIndex++);
        var columnIndex = 0;
        foreach (var property in properties)
            currentRow.CreateCell(columnIndex++).SetCellValue(property.Name);

        foreach (var obj in sourceObjects)
        {
            columnIndex = 0;
            currentRow = sheet.CreateRow(rowIndex++);
            foreach (var property in properties)
                currentRow.CreateCell(columnIndex++)
                    .SetCellValue(property.GetMethod.Invoke(obj, Array.Empty<object>())?.ToString());
        }
    }
}