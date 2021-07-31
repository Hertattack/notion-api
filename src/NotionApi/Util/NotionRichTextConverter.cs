using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using NotionApi.Rest.Common.Objects.Text;
using RestUtil.Conversion;

namespace NotionApi.Util
{
    public class NotionRichTextConverter : CustomTypeDeserializer<RichTextObject>
    {
        public NotionRichTextConverter(ILogger<CustomTypeDeserializer<RichTextObject>> logger) : base(logger)
        {
        }

        protected override RichTextObject CreateInstance(JObject jObject)
        {
            return (string) jObject["type"] switch
            {
                "text" => new RichTextTextObject(),
                "mention" => new RichTextMentionObject(),
                "equation" => new RichTextEquationObject(),
                _ => new RichTextObject()
            };
        }
    }
}