using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using NotionApi.Rest.Response.Objects;
using RestUtil.Conversion;

namespace NotionApi.Util;

public class BasicNotionObjectConverter : CustomTypeDeserializer<BasicNotionObject>
{
    public BasicNotionObjectConverter(ILogger<CustomTypeDeserializer<BasicNotionObject>> logger) : base(logger)
    {
    }

    protected override BasicNotionObject CreateInstance(JObject jObject)
    {
        return (string) jObject["type"] switch
        {
            "person" => new PersonObject(),
            "bot" => new BotObject(),
            "url" => new LinkObject(),
            _ => new BasicNotionObject()
        };
    }
}