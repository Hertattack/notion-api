using Newtonsoft.Json;

namespace NotionApi.Rest.Common.Objects.Text.Mention
{
    public class UserMention : Mention
    {
        [JsonProperty("user")] public UserObject User { get; set; }
    }
}