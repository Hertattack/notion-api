using Newtonsoft.Json;

namespace NotionApi.Rest.Response.Objects;

public class BotObject : UserObject
{
    [JsonProperty("bot")] public object Bot { get; set; }
}