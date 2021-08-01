using Newtonsoft.Json;

namespace NotionApi.Rest.Objects
{
    public class BotObject : UserObject
    {
        [JsonProperty("bot")] public object Bot { get; set; }
    }
}