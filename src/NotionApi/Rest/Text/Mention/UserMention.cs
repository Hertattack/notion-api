using Newtonsoft.Json;
using NotionApi.Rest.Objects;

namespace NotionApi.Rest.Text.Mention
{
    public class UserMention : Mention
    {
        [JsonProperty("user")] public UserObject User { get; set; }
    }
}