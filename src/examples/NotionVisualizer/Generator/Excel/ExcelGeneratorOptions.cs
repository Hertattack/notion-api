using System.Collections.Generic;

namespace NotionVisualizer.Generator.Excel
{
    public class ExcelGeneratorOptions
    {
        public string Filename { get; set; } = "graph.xlsx";

        public IList<string> NodeProperties { get; set; } = new List<string>();

        public IList<string> EdgeProperties { get; set; } = new List<string>();
    }
}