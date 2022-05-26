using Newtonsoft.Json;
using Util;

namespace NotionApi.Rest.Response.Page.Properties;

public class UrlPropertyValue : NotionPropertyValue
{
    [JsonProperty("url")] public Option<string> Url { get; set; }
}