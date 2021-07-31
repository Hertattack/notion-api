using Newtonsoft.Json;
using Util;

namespace NotionApi.Rest.Common.Properties
{
    public class DateProperty : NotionProperty
    {
        [JsonProperty("start")] public string Start { get; set; }

        [JsonProperty("end")] public Option<string> End { get; set; }
    }
}