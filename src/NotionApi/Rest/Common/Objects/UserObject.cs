using Newtonsoft.Json;

namespace NotionApi.Rest.Common.Objects
{
    public class UserObject : BasicNotionObject
    {
        [JsonProperty("object")] public string ObjectType { get; set; }

        [JsonProperty("id")] public string Id { get; set; }

        [JsonProperty("name")] public string Name { get; set; }

        [JsonProperty("avatar_url")] public string AvatarUrl { get; set; }
    }
}