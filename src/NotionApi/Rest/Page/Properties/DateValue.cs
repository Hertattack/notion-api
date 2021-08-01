using Newtonsoft.Json;
using Util;

namespace NotionApi.Rest.Page.Properties
{
    public class DateValue
    {
        [JsonProperty("start")] public string Start { get; set; }

        [JsonProperty("end")] public Option<string> End { get; set; }
    }
}