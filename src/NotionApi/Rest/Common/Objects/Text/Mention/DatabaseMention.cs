﻿using Newtonsoft.Json;

namespace NotionApi.Rest.Common.Objects.Text.Mention
{
    public class DatabaseMention : Mention
    {
        [JsonProperty("page")] public DatabaseReference Page { get; set; }
    }

    public class DatabaseReference
    {
        [JsonProperty("id")] public string Id { get; set; }
    }
}