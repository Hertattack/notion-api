using System.Collections;
using NotionApi.Rest.Response.Page.Properties;
using Util;
using Util.Extensions;

namespace NotionGraphApi.Interface;

public class Row
{
    public Dictionary<string, FieldValueSet> FieldValueSets { get; } = new();

    private FieldValueSet GetFieldValueSet(string setAlias)
    {
        if (FieldValueSets.TryGetValue(setAlias, out var fieldValueSet))
            return fieldValueSet;

        fieldValueSet = new FieldValueSet(setAlias);
        FieldValueSets.Add(setAlias, fieldValueSet);
        return fieldValueSet;
    }

    public void AddFieldValue(string setAlias, string fieldName, object? value)
    {
        GetFieldValueSet(setAlias)[fieldName] = MapValue(value);
    }

    private static FieldValue? MapValue(object? inValue)
    {
        var value = inValue;

        if (inValue is IOption option)
            value = option.HasValue ? option.GetValue() : null;

        if (value is null) return null;

        var nonNullValue = value.ThrowIfNull();

        if (nonNullValue is not NotionPropertyValue)
        {
            if (nonNullValue is DateValue dateValue)
                return new DateFieldValue(dateValue.Start, dateValue.End.HasValue ? dateValue.End.Value : null);

            if (nonNullValue is string
                || nonNullValue.GetType().IsPrimitive
                || nonNullValue is not IEnumerable enumerable)
                return new ObjectFieldValue(value);

            var list = new ListFieldValue();
            foreach (var v in enumerable)
            {
                var listValue = MapValue(v);
                if (listValue is not null)
                    list.Add(listValue);
            }

            return list;
        }

        if (nonNullValue is CheckboxPropertyValue checkBox)
            return new ObjectFieldValue(checkBox.Checkbox.HasValue && checkBox.Checkbox.Value);

        return new ObjectFieldValue(nonNullValue.ToString());
    }
}