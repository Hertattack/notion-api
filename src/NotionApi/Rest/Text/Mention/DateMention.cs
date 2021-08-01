using Newtonsoft.Json;
using NotionApi.Rest.Page.Properties;

namespace NotionApi.Rest.Text.Mention
{
    public class DateMention
    {
        [JsonProperty("date")] public DatePropertyValue Date { get; set; }
    }
}