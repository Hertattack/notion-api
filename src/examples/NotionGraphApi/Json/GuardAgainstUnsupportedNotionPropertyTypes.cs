using System.Text.Json;
using System.Text.Json.Serialization;
using NotionApi.Rest.Response.Page.Properties;

namespace NotionGraphApi.Json;

public class GuardAgainstUnsupportedNotionPropertyTypes : JsonConverter<NotionPropertyValue>
{
    public override NotionPropertyValue? Read(ref Utf8JsonReader reader, Type typeToConvert,
        JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }

    public override void Write(Utf8JsonWriter writer, NotionPropertyValue value, JsonSerializerOptions options)
    {
        throw new NotImplementedException($"Unimplemented Notion Property Value type: {value.GetType().FullName}");
    }
}