using Newtonsoft.Json;
using Util;

namespace NotionApi.Rest.Properties
{
    public class PhoneNumberPropertyValue : NotionPropertyValue
    {
        [JsonProperty("phone_number")] public Option<string> PhoneNumber { get; set; }
    }
}