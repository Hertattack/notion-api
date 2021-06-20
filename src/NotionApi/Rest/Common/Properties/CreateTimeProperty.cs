using Newtonsoft.Json;
using Util;

namespace NotionApi.Rest.Common.Properties
{
    public class CreateTimeProperty : NotionProperty
    {
        [JsonProperty(PropertyName = "created_time")]
        public Option<string> CreatedTime { get; set; }
    }
}