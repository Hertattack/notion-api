using Newtonsoft.Json;
using Util;

namespace NotionApi.Rest.Response.Page.Properties
{
    public class DatePropertyValue : NotionPropertyValue
    {
        [JsonProperty("date")] public Option<DateValue> Date { get; set; }
    }
}