using Newtonsoft.Json;
using Util;

namespace NotionApi.Rest.Page.Properties
{
    public class NumberPropertyValue : NotionPropertyValue
    {
        [JsonProperty("number")] public Option<string> Number { get; set; }
    }
}