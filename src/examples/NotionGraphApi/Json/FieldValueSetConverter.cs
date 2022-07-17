using System.Text.Json;
using System.Text.Json.Serialization;
using NotionGraphApi.Interface;

namespace NotionGraphApi.Json;

public class FieldValueSetConverter : JsonConverter<FieldValueSet>
{
    public override FieldValueSet? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }

    public override void Write(Utf8JsonWriter writer, FieldValueSet value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();

        writer.WritePropertyName("alias");
        writer.WriteStringValue(value.Alias);

        writer.WritePropertyName("values");
        writer.WriteStartObject();

        foreach (var fieldValueEntry in value.FieldValues)
        {
            writer.WritePropertyName(fieldValueEntry.Key);
            if (fieldValueEntry.Value is null)
                writer.WriteNullValue();
            else
                JsonSerializer.Serialize(writer, fieldValueEntry.Value, fieldValueEntry.Value.GetType(), options);
        }

        writer.WriteEndObject();

        writer.WriteEndObject();
    }
}