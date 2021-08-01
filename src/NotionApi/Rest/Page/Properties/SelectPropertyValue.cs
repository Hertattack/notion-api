using Newtonsoft.Json;
using Util;

namespace NotionApi.Rest.Properties
{
    public class SelectPropertyValue : NotionPropertyValue
    {
        [JsonProperty(PropertyName = "options")]
        public Option<SelectOptionValue> SelectedOption { get; set; }
    }
}