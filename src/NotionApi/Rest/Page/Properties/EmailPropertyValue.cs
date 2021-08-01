using Newtonsoft.Json;
using Util;

namespace NotionApi.Rest.Properties
{
    public class EmailPropertyValue : NotionPropertyValue
    {
        [JsonProperty("email")] public Option<string> Email { get; set; }
    }
}