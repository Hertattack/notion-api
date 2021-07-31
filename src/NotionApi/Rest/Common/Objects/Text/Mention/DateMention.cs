using Newtonsoft.Json;
using NotionApi.Rest.Common.Properties;

namespace NotionApi.Rest.Common.Objects.Text.Mention
{
    public class DateMention
    {
        [JsonProperty("date")] public DateProperty Date { get; set; }
    }
}