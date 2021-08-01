using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using NotionApi.Rest.Text.Mention;
using RestUtil.Conversion;

namespace NotionApi.Util
{
    public class NotionMentionConverter : CustomTypeDeserializer<Mention>
    {
        public NotionMentionConverter(ILogger<CustomTypeDeserializer<Mention>> logger) : base(logger)
        {
        }

        protected override Mention CreateInstance(JObject jObject)
        {
            return (string) jObject["type"] switch
            {
                "user" => new UserMention(),
                "page" => new PageMention(),
                "database" => new DatabaseMention(),
                "date" => new DatabaseMention(),
                _ => new Mention()
            };
        }
    }
}