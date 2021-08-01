using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using NotionApi.Rest.Page.Properties.Formula;
using RestUtil.Conversion;

namespace NotionApi.Util
{
    public class NotionFormulaValueConverter : CustomTypeDeserializer<FormulaValue>
    {
        public NotionFormulaValueConverter(ILogger<CustomTypeDeserializer<FormulaValue>> logger) : base(logger)
        {
        }

        protected override FormulaValue CreateInstance(JObject jObject)
        {
            return (string) jObject["type"] switch
            {
                "string" => new StringFormulaValue(),
                "number" => new NumberFormulaValue(),
                "boolean" => new BooleanFormulaValue(),
                "date" => new DateFormulaValue(),
                _ => new FormulaValue()
            };
        }
    }
}