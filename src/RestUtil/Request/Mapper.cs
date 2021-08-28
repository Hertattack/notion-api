using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using RestUtil.Mapping;
using RestUtil.Request.Attributes;
using Util;

namespace RestUtil.Request
{
    public class Mapper : IMapper
    {
        private readonly Dictionary<Type, IMappingStrategy> _strategies = new();
        
        public object Map(RequestParameter parameter) =>
            parameter.Strategy != null
                ? Map(parameter.Value, GetCachedMappingStrategy(parameter.Strategy))
                : Map(parameter.Value);

        public object Map(object obj) =>
            Map(obj, null);

        object Map(object objectToMap, IMappingStrategy returnValueStrategy)
        {
            var type = objectToMap.GetType();
            var mappingAttributeType = typeof(MappingAttribute);
            var typeMapping = (MappingAttribute) type.GetCustomAttributes(mappingAttributeType, inherit: true).FirstOrDefault();

            var values = new Dictionary<string, object>();

            foreach (var property in type.GetProperties(BindingFlags.Instance | BindingFlags.Public))
            {
                var propertyMapping = (MappingAttribute) property.GetCustomAttributes(typeof(MappingAttribute)).FirstOrDefault();
                if (propertyMapping is null)
                    continue;

                if (property.GetMethod is null)
                    continue;

                var propertyType = property.PropertyType;
                var propertyValue = property.GetMethod.Invoke(objectToMap, Array.Empty<object>());
                var strategy = GetStrategy(propertyMapping);
                var name = propertyMapping.Name.HasValue ? propertyMapping.Name.Value : property.Name;

                IOption value;

                var optionValue = ToOption(propertyType, propertyValue);
                if (optionValue.HasValue)
                {
                    if (!optionValue.Value.HasValue)
                        continue;

                    value = strategy.GetValue(propertyType.GetGenericArguments()[0], optionValue.Value.GetValue());
                }
                else
                    value = strategy.GetValue(propertyType, propertyValue);

                if (value.HasValue)
                    values[name] = value.GetValue();
            }

            if (typeMapping is null && returnValueStrategy == null)
                return values;

            var typeMappingStrategy = returnValueStrategy ?? GetStrategy(typeMapping, useDefaultIfNonSpecified: false);

            if (typeMappingStrategy == null)
                return values;

            var mappedValue = typeMappingStrategy.GetValue(type, values);
            return mappedValue.HasValue ? mappedValue.Value : null;
        }

        public Option<IOption> ToOption(Type type, object value)
        {
            if (!type.IsGenericType || type.GetGenericTypeDefinition() != typeof(Option<>))
                return Option.None;

            return Option<IOption>.From((IOption) value);
        }

        public object MapEnumeration(Type type, Enum valueToMap)
        {
            if (!type.IsEnum)
                throw new ArgumentException($"Type: {type.FullName} is not an enumeration.", nameof(type));

            var stringValue = valueToMap.ToString();
            var field = type.GetFields().First(f => f.Name == stringValue);

            var mapping = (MappingAttribute) field.GetCustomAttributes(typeof(MappingAttribute), true).FirstOrDefault();
            if (mapping != null)
            {
                var strategy = GetStrategy(mapping, useDefaultIfNonSpecified: false);
                if (strategy != null)
                    return strategy.GetValue(type, valueToMap).Value;

                if (mapping.Name.HasValue)
                    return mapping.Name.Value;
            }

            return valueToMap.ToString();
        }

        private IMappingStrategy GetStrategy(MappingAttribute attributeMapping, bool useDefaultIfNonSpecified = true)
        {
            if (!useDefaultIfNonSpecified && attributeMapping.Strategy == null)
                return null;

            var strategyType = attributeMapping.Strategy ?? typeof(DefaultMappingStrategy);

            return GetCachedMappingStrategy(strategyType);
        }

        private IMappingStrategy GetCachedMappingStrategy(Type strategyType)
        {
            IMappingStrategy strategy;

            if (_strategies.TryGetValue(strategyType, out strategy))
                return strategy;

            strategy = (IMappingStrategy) Activator.CreateInstance(strategyType, this);
            _strategies[strategyType] = strategy;

            return strategy;
        }
    }
}