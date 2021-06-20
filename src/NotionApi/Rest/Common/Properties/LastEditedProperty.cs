using Newtonsoft.Json;

namespace NotionApi.Rest.Common.Properties
{
    public class LastEditedProperty : NotionProperty
    {
        [JsonProperty(PropertyName = "last_edited_time", NullValueHandling = NullValueHandling.Ignore)]
        public string LastEditedTime { get; set; }
    }
}