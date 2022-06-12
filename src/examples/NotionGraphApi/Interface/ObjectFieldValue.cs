namespace NotionGraphApi.Interface;

public class ObjectFieldValue : FieldValue
{
    public object? Value { get; }

    public ObjectFieldValue(object? value)
    {
        Value = value;
    }
}