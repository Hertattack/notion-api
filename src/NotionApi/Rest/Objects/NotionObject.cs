﻿using Newtonsoft.Json;

namespace NotionApi.Rest.Objects
{
    public class NotionObject
    {
        [JsonProperty(PropertyName = "created_time")]
        public string CreatedTime { get; set; }

        [JsonProperty(PropertyName = "last_edited_time")]
        public string LastEditedTime { get; set; }

        [JsonProperty(PropertyName = "object")]
        public string ObjectType { get; set; }

        [JsonProperty(PropertyName = "id")] public string Id { get; set; }
    }
}