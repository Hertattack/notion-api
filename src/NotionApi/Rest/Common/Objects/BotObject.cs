using Newtonsoft.Json;

namespace NotionApi.Rest.Common.Objects
{
    public class BotObject : UserObject
    {
        [JsonProperty("bot")] public object Bot { get; set; }
    }
}