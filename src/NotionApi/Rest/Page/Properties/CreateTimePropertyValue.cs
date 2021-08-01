using Newtonsoft.Json;
using Util;

namespace NotionApi.Rest.Page.Properties
{
    public class CreateTimePropertyValue : NotionPropertyValue
    {
        [JsonProperty(PropertyName = "created_time")]
        public Option<string> CreatedTime { get; set; }
    }
}