using Newtonsoft.Json;
using NotionApi.Rest.Response.Page.Properties;

namespace NotionApi.Rest.Response.Text.Mention
{
    public class DateMention
    {
        [JsonProperty("date")] public DatePropertyValue Date { get; set; }
    }
}