using Newtonsoft.Json.Linq;
using NotionApi.Rest.Common.Properties;

namespace NotionApi.Util
{
    internal static class CreateTimePropertyConverter
    {
        public static NotionProperty Convert(JObject data)
        {
            var value = new LastEditedProperty();

            if (!(data["last_edit_time"] is null))
                value.LastEditedTime = (string) data["last_edit_time"];

            value.Id = (string) data["id"];
            return value;
        }
    }
}