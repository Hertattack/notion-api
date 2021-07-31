using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using NotionApi.Rest.Common.Properties;
using RestUtil.Conversion;

namespace NotionApi.Util
{
    public class NotionPropertyConverter : CustomTypeDeserializer<NotionProperty>
    {
        public NotionPropertyConverter(ILogger<CustomTypeDeserializer<NotionProperty>> logger) : base(logger)
        {
        }

        protected override NotionProperty CreateInstance(JObject jObject)
        {
            return (string) jObject["type"] switch
            {
                "created_time" => new CreateTimeProperty(),
                "created_by" => new NotionProperty(),
                "last_edited_time" => new LastEditedProperty(),
                "last_edited_by" => new NotionProperty(),
                "relation" => jObject["relation"]?.Type == JTokenType.Array
                    ? (NotionProperty) new OneToManyRelationProperty()
                    : new ManyToOneRelationshipProperty(),
                "rich_text" => new RichTextProperty(),
                "title" => new TitleProperty(),
                "number" => new NotionProperty(),
                "select" => new NotionProperty(),
                "multi_select" => new NotionProperty(),
                "date" => new NotionProperty(),
                "formula" => new NotionProperty(),
                "rollup" => new NotionProperty(),
                "people" => new NotionProperty(),
                "files" => new NotionProperty(),
                "checkbox" => new NotionProperty(),
                "url" => new NotionProperty(),
                "email" => new NotionProperty(),
                "phone_number" => new NotionProperty(),
                _ => new NotionProperty()
            };
        }
    }
}