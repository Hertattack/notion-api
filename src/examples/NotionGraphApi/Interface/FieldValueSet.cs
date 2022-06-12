namespace NotionGraphApi.Interface;

public class FieldValueSet
{
    public Dictionary<string, FieldValue?> FieldValues = new();

    public string Alias { get; }

    public FieldValueSet(string setAlias)
    {
        Alias = setAlias;
    }

    public FieldValue? this[string fieldName]
    {
        get => FieldValues.ContainsKey(fieldName) ? FieldValues[fieldName] : null;
        set => FieldValues[fieldName] = value;
    }
}