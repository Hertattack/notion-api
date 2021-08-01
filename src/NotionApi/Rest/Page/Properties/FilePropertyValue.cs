using System.Collections.Generic;
using Newtonsoft.Json;
using Util;

namespace NotionApi.Rest.Page.Properties
{
    public class FilePropertyValue : NotionPropertyValue
    {
        [JsonProperty("files")] public Option<IList<FileReference>>  Files { get; set; }
    }
}