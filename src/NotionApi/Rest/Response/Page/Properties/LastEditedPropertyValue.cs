using Newtonsoft.Json;
using Util;

namespace NotionApi.Rest.Response.Page.Properties
{
    public class LastEditedPropertyValue : NotionPropertyValue
    {
        [JsonProperty(PropertyName = "last_edited_time", NullValueHandling = NullValueHandling.Ignore)]
        public Option<string> LastEditedTime { get; set; }
    }
}