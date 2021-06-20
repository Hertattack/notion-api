using Newtonsoft.Json;

namespace NotionApi.Rest.Common.Properties
{
    public class CreateTimeProperty : NotionProperty
    {
        [JsonProperty(PropertyName = "created_time")]
        public string CreatedTime { get; set; }
    }
}