using System.Collections.Generic;
using Newtonsoft.Json;
using Util;

namespace NotionApi.Rest.Response.Database.Properties;

public class SelectConfiguration
{
    [JsonProperty("options")] public Option<IList<SelectOptionDefinition>> Options { get; set; }
}