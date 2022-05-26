using Newtonsoft.Json;

namespace NotionApi.Rest.Response.Text.Mention;

public class PageMention : Mention
{
    [JsonProperty("page")] public PageReference Page { get; set; }
}

public class PageReference
{
    [JsonProperty("id")] public string Id { get; set; }
}