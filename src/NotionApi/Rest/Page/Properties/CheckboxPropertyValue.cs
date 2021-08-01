using Newtonsoft.Json;
using Util;

namespace NotionApi.Rest.Page.Properties
{
    public class CheckboxPropertyValue : NotionPropertyValue
    {
        [JsonProperty("checkbox")] public Option<bool> Checkbox { get; set; }
    }
}