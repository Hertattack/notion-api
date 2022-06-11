namespace NotionGraphApi.Interface;

public class Row
{
    public Dictionary<string, Dictionary<string, FieldValue?>> Fields { get; } = new();

    private Dictionary<string, FieldValue?> GetFieldValueSet(string setAlias)
    {
        if (Fields.TryGetValue(setAlias, out var fieldValueSet))
            return fieldValueSet;

        fieldValueSet = new Dictionary<string, FieldValue?>();
        Fields.Add(setAlias, fieldValueSet);
        return fieldValueSet;
    }

    public void AddFieldValue(string setAlias, string fieldName, object? value)
    {
        GetFieldValueSet(setAlias).Add(fieldName, value is null ? null : new ObjectFieldValue(value));
    }
}