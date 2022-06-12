using System.Text.Json;
using System.Text.Json.Serialization;
using NotionGraphApi.Interface;

namespace NotionGraphApi.Json;

public class ListFieldValueConverter : JsonConverter<ListFieldValue>
{
    public override ListFieldValue? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }

    public override void Write(Utf8JsonWriter writer, ListFieldValue value, JsonSerializerOptions options)
    {
        writer.WriteStartArray();

        foreach (var item in value) JsonSerializer.Serialize(writer, item, item.GetType(), options);

        writer.WriteEndArray();
    }
}