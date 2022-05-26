using Newtonsoft.Json;
using NotionApi.Rest.Response.Database.Properties;
using Util;

namespace NotionApi.Rest.Response.Page.Properties;

public class NotionPropertyValue
{
    [JsonProperty(PropertyName = "id")] public Option<string> Id { get; set; }

    [JsonProperty(PropertyName = "type")] public string Type { get; set; }

    [JsonIgnore] public Option<PageObject> Container { get; set; }
    [JsonIgnore] public Option<NotionPropertyConfiguration> Configuration { get; set; }
}