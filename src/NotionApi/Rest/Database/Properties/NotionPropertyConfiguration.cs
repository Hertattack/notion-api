using Newtonsoft.Json;
using Util;

namespace NotionApi.Rest.Database.Properties
{
    public class NotionPropertyConfiguration
    {
        [JsonProperty(PropertyName = "id")] public Option<string> Id { get; set; }

        [JsonProperty(PropertyName = "type")] public string Type { get; set; }

        [JsonIgnore] public Option<DatabaseObject> Container { get; set; }
    }
}