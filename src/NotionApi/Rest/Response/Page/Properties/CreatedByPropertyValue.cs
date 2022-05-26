using Newtonsoft.Json;
using NotionApi.Rest.Response.Objects;
using Util;

namespace NotionApi.Rest.Response.Page.Properties;

public class CreatedByPropertyValue : NotionPropertyValue
{
    [JsonProperty("created_by")] public Option<UserObject> CreatedBy { get; set; }
}