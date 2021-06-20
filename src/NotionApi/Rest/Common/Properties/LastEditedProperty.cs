using Newtonsoft.Json;
using Util;

namespace NotionApi.Rest.Common.Properties
{
    public class LastEditedProperty : NotionProperty
    {
        [JsonProperty(PropertyName = "last_edited_time", NullValueHandling = NullValueHandling.Ignore)]
        public Option<string> LastEditedTime { get; set; }
    }
}