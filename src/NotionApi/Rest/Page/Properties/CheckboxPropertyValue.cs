using Newtonsoft.Json;
using Util;

namespace NotionApi.Rest.Properties
{
    public class CheckboxPropertyValue : NotionPropertyValue
    {
        [JsonProperty("checkbox")] public Option<bool> Checkbox { get; set; }
    }
}