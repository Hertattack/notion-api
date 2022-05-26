using Newtonsoft.Json;
using NotionApi.Rest.Response.Objects;

namespace NotionApi.Rest.Response.Text.Mention;

public class UserMention : Mention
{
    [JsonProperty("user")] public UserObject User { get; set; }
}