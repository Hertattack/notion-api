using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using NotionApi.Rest.Database.Properties;
using RestUtil.Conversion;

namespace NotionApi.Util
{
    public class NotionDatabasePropertyConverter : CustomTypeDeserializer<NotionPropertyConfiguration>
    {
        public NotionDatabasePropertyConverter(ILogger<CustomTypeDeserializer<NotionPropertyConfiguration>> logger) : base(logger)
        {
        }

        protected override NotionPropertyConfiguration CreateInstance(JObject jObject)
        {
            return (string) jObject["type"] switch
            {
                "number" => new NumberPropertyConfiguration(),
                "select" => new SelectPropertyConfiguration(),
                "multi-select" => new MultiSelectPropertyConfiguration(),
                "formula" => new FormulaPropertyConfiguration(),
                "relation" => new RelationPropertyConfiguration(),
                "rollup" => new RollupPropertyConfiguration(),
                _ => new NotionPropertyConfiguration()
            };
        }
    }
}