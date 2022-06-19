using NotionGraphDatabase.Storage.DataModel;

namespace NotionGraphDatabase.Storage.Filtering;

public class StringFilter : DatabaseFilterCondition
{
    public PropertyDefinition PropertyDefinition { get; }
    public string Value { get; }

    public StringFilter(PropertyDefinition propertyDefinition, string value)
    {
        PropertyDefinition = propertyDefinition;
        Value = value;
    }
}