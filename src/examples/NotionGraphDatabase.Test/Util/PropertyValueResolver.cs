using System;
using NotionGraphDatabase.QueryEngine.Query.Expression;

namespace NotionGraphDatabase.Test.Util;

public class PropertyValueResolver : IPropertyValueResolver
{
    private readonly string _alias;
    private readonly string _propertyName;
    private readonly object? _value;

    private PropertyValueResolver(string alias, string propertyName, object? value)
    {
        _alias = alias;
        _propertyName = propertyName;
        _value = value;
    }

    public object? GetValue(string alias, string propertyName)
    {
        if (alias != _alias)
            throw new Exception($"Expected alias: '{_alias}', got: '{alias}'.");

        if (propertyName != _propertyName)
            throw new Exception($"Expected property name: '{_propertyName}', got: '{propertyName}'.");

        return _value;
    }

    public static IPropertyValueResolver For(string alias, string propertyName, object? value)
    {
        return new PropertyValueResolver(alias, propertyName, value);
    }
}