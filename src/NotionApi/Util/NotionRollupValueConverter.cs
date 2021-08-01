using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using NotionApi.Rest.Page.Properties.Rollup;
using RestUtil.Conversion;

namespace NotionApi.Util
{
    public class NotionRollupValueConverter : CustomTypeDeserializer<RollupValue>
    {
        public NotionRollupValueConverter(ILogger<CustomTypeDeserializer<RollupValue>> logger) : base(logger)
        {
        }

        protected override RollupValue CreateInstance(JObject jObject)
        {
            return (string) jObject["type"] switch
            {
                "number" => new NumberRollupValue(),
                "array" => new ArrayRollupValue(),
                "date" => new DateRollupValue(),
                _ => new RollupValue()
            };
        }
    }
}