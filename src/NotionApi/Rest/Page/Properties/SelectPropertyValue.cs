using Newtonsoft.Json;
using Util;

namespace NotionApi.Rest.Page.Properties
{
    public class SelectPropertyValue : NotionPropertyValue
    {
        [JsonProperty(PropertyName = "options")]
        public Option<SelectOptionValue> SelectedOption { get; set; }
    }
}