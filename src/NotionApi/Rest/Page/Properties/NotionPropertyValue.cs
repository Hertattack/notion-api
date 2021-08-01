﻿using Newtonsoft.Json;
using Util;

namespace NotionApi.Rest.Properties
{
    public class NotionPropertyValue
    {
        [JsonProperty(PropertyName = "id")] public Option<string> Id { get; set; }

        [JsonProperty(PropertyName = "type")] public string Type { get; set; }
    }
}