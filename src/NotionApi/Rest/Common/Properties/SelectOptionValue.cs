﻿using Newtonsoft.Json;

namespace NotionApi.Rest.Common.Properties
{
    public class SelectOptionValue
    {
        [JsonProperty(PropertyName = "id")] public string Id { get; set; }

        [JsonProperty(PropertyName = "name")] public string Name { get; set; }

        [JsonProperty(PropertyName = "color")] public string Color { get; set; }
    }
}