using Newtonsoft.Json;

namespace NotionApi.Rest.Objects
{
    public class PersonObject : UserObject
    {
        [JsonProperty("person")] public object Person { get; set; }
        [JsonProperty("person.email")] public object Email { get; set; }
    }
}