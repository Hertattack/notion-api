using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using NotionApi.Rest.Response.Page.Properties;
using NotionApi.Rest.Response.Page.Properties.Relation;
using RestUtil.Conversion;

namespace NotionApi.Util;

public class NotionPagePropertyConverter : CustomTypeDeserializer<NotionPropertyValue>
{
    public NotionPagePropertyConverter(ILogger<CustomTypeDeserializer<NotionPropertyValue>> logger) : base(logger)
    {
    }

    protected override NotionPropertyValue CreateInstance(JObject jObject)
    {
        return (string) jObject["type"] switch
        {
            "created_time" => new CreateTimePropertyValue(),
            "created_by" => new CreatedByPropertyValue(),
            "last_edited_time" => new LastEditedPropertyValue(),
            "last_edited_by" => new LastEditedByPropertyValue(),
            "relation" => new OneToManyRelationPropertyValue(),
            "rich_text" => new RichTextPropertyValue(),
            "title" => new TitlePropertyValue(),
            "number" => new NumberPropertyValue(),
            "select" => new SelectPropertyValue(),
            "multi_select" => new MultiSelectPropertyValue(),
            "date" => new DatePropertyValue(),
            "formula" => new FormulaPropertyValue(),
            "rollup" => new RollupPropertyValue(),
            "people" => new PeoplePropertyValue(),
            "files" => new FilePropertyValue(),
            "checkbox" => new CheckboxPropertyValue(),
            "url" => new UrlPropertyValue(),
            "email" => new EmailPropertyValue(),
            "phone_number" => new PhoneNumberPropertyValue(),
            _ => new NotionPropertyValue()
        };
    }
}