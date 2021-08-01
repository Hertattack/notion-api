using Newtonsoft.Json;
using NotionApi.Rest.Objects;
using Util;

namespace NotionApi.Rest.Properties
{
    public class LastEditedByPropertyValue : NotionPropertyValue
    {
        [JsonProperty("last_edited_by")] public Option<UserObject> LastEditedBy { get; set; }
    }
}