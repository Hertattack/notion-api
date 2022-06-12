using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using RestUtil.Mapping;
using RestUtil.Request.Attributes;
using Util;
using Util.Extensions;

namespace RestUtil.Request;

public class Mapper : IMapper
{
    private readonly Dictionary<Type, IMappingStrategy?> _strategies = new();

    public object? Map(RequestParameter parameter)
    {
        return parameter.Strategy != null
            ? Map(parameter.Value, GetCachedMappingStrategy(parameter.Strategy))
            : Map(parameter.Value);
    }

    public object? Map(object obj)
    {
        return Map(obj, null);
    }

    private object? Map(object objectToMap, IMappingStrategy? returnValueStrategy)
    {
        var type = objectToMap.GetType();
        var mappingAttributeType = typeof(MappingAttribute);
        var typeMapping = type.GetCustomAttributes(mappingAttributeType, true).FirstOrDefault() as MappingAttribute;

        var values = new Dictionary<string, object?>();

        foreach (var property in type.GetProperties(BindingFlags.Instance | BindingFlags.Public))
        {
            if (property.GetCustomAttributes(typeof(MappingAttribute)).FirstOrDefault() is not MappingAttribute
                propertyMapping)
                continue;

            if (property.GetMethod is null)
                continue;

            var propertyType = property.PropertyType;
            var propertyValue = property.GetMethod.Invoke(objectToMap, Array.Empty<object>());
            var strategy = GetStrategy(propertyMapping).ThrowIfNull();
            var name = propertyMapping.Name.HasValue ? propertyMapping.Name.Value : property.Name;

            IOption value;

            var optionValue = ToOption(propertyType, propertyValue);
            if (optionValue.HasValue)
            {
                if (!optionValue.Value.HasValue)
                    continue;

                value = strategy.GetValue(propertyType.GetGenericArguments()[0], optionValue.Value.GetValue());

                if (value.HasValue)
                    values[name] = value.GetValue();
            }
            else
            {
                value = strategy.GetValue(propertyType, propertyValue);
                values[name] = value.HasValue ? value.GetValue() : GetDefault(propertyType);
            }
        }

        if (typeMapping is null && returnValueStrategy == null)
            return values;

        var typeMappingStrategy = returnValueStrategy ?? GetStrategy(typeMapping.ThrowIfNull(), false);

        if (typeMappingStrategy == null)
            return values;

        var mappedValue = typeMappingStrategy.GetValue(type, values);
        return mappedValue.HasValue ? mappedValue.Value : null;
    }

    private static object? GetDefault(Type propertyType)
    {
        return propertyType.IsPrimitive ? Activator.CreateInstance(propertyType) : null;
    }

    public Option<IOption> ToOption(Type type, object? value)
    {
        if (!type.IsGenericType || type.GetGenericTypeDefinition() != typeof(Option<>))
            return Option.None;

        return Option<IOption>.From((IOption) value.ThrowIfNull());
    }

    public object MapEnumeration(Type type, Enum valueToMap)
    {
        if (!type.IsEnum)
            throw new ArgumentException($"Type: {type.FullName} is not an enumeration.", nameof(type));

        var stringValue = valueToMap.ToString();
        var field = type.GetFields().First(f => f.Name == stringValue);

        var mappingAttributes = field.GetCustomAttributes(typeof(MappingAttribute), true).ToList();

        if (!mappingAttributes.Any())
            return valueToMap.ToString();

        var mapping = (MappingAttribute) mappingAttributes.First();
        var strategy = GetStrategy(mapping, false);
        if (strategy != null)
            return strategy.GetValue(type, valueToMap).Value;

        return mapping.Name.HasValue
            ? mapping.Name.Value
            : valueToMap.ToString();
    }

    private IMappingStrategy? GetStrategy(MappingAttribute attributeMapping, bool useDefaultIfNonSpecified = true)
    {
        if (!useDefaultIfNonSpecified && attributeMapping.Strategy == null)
            return null;

        var strategyType = attributeMapping.Strategy ?? typeof(DefaultMappingStrategy);

        return GetCachedMappingStrategy(strategyType);
    }

    private IMappingStrategy GetCachedMappingStrategy(Type strategyType)
    {
        if (_strategies.TryGetValue(strategyType, out var foundStrategy))
            return foundStrategy.ThrowIfNull();

        var strategy = (IMappingStrategy) Activator.CreateInstance(strategyType, this).ThrowIfNull();
        _strategies[strategyType] = strategy;

        return strategy;
    }
}