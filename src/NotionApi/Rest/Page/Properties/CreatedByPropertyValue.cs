using Newtonsoft.Json;
using NotionApi.Rest.Objects;
using Util;

namespace NotionApi.Rest.Properties
{
    public class CreatedByPropertyValue : NotionPropertyValue
    {
        [JsonProperty("created_by")] public Option<UserObject> CreatedBy { get; set; }
    }
}