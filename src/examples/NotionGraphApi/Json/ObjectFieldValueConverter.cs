using System.Text.Json;
using System.Text.Json.Serialization;
using NotionGraphApi.Interface;

namespace NotionGraphApi.Json;

public class ObjectFieldValueConverter : JsonConverter<ObjectFieldValue>
{
    public override ObjectFieldValue? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }

    public override void Write(Utf8JsonWriter writer, ObjectFieldValue value, JsonSerializerOptions options)
    {
        if (value.Value is null)
            writer.WriteNullValue();
        else
            JsonSerializer.Serialize(writer, value.Value, value.Value.GetType(), options);
    }
}