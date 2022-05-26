using Newtonsoft.Json;
using NotionApi.Rest.Response.Objects;
using Util;

namespace NotionApi.Rest.Response.Page.Properties;

public class LastEditedByPropertyValue : NotionPropertyValue
{
    [JsonProperty("last_edited_by")] public Option<UserObject> LastEditedBy { get; set; }
}