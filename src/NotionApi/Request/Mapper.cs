using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NotionApi.Request.Attributes;
using NotionApi.Request.Mapping;
using NotionApi.Util;

namespace NotionApi.Request
{
    public class Mapper : IMapper
    {
        private Dictionary<Type, IMappingStrategy> Strategies = new Dictionary<Type, IMappingStrategy>();

        public object Map(object objectToMap)
        {
            var type = objectToMap.GetType();
            var mappingAttributeType = typeof(MappingAttribute);
            var typeMapping = (MappingAttribute) type.GetCustomAttributes(mappingAttributeType, inherit: true).FirstOrDefault();

            var values = new Dictionary<string, object>();

            foreach (var property in type.GetProperties(BindingFlags.Public))
            {
                var propertyMapping = (MappingAttribute) property.GetCustomAttributes(typeof(MappingAttribute)).FirstOrDefault();
                if (propertyMapping == null)
                    continue;

                if (property.GetMethod is null)
                    continue;

                var propertyType = property.PropertyType;
                var propertyValue = property.GetMethod.Invoke(objectToMap, Array.Empty<object>());
                var strategy = GetStrategy(propertyMapping);
                var name = propertyMapping.Name.HasValue ? propertyMapping.Name.Value : property.Name;
                if (IsOption(propertyType, propertyValue, out var isNoneOption))
                {
                    if (isNoneOption)
                        continue;

                    values[name] = strategy.GetValue(propertyType.GetGenericTypeDefinition().GetGenericArguments()[0], propertyValue);
                }
                else
                    values[name] = strategy.GetValue(propertyType, propertyValue);
            }

            if (typeMapping is null)
                return values;

            var typeMappingStrategy = GetStrategy(typeMapping, useDefaultIfNonSpecified: false);

            if (typeMappingStrategy == null)
                return values;

            var mappedValue = typeMappingStrategy.GetValue(type, values);
            return mappedValue.HasValue ? mappedValue.Value : null;
        }

        private bool IsOption(Type type, object value, out bool isNoneOption)
        {
            isNoneOption = false;

            if (!type.IsGenericType || !type.IsAssignableFrom(typeof(Option<>)))
                return false;

            if (!((IOption) value).HasValue)
                isNoneOption = true;

            return true;
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
                    return strategy.GetValue(type, valueToMap);

                return mapping.Name;
            }

            return valueToMap.ToString();
        }

        private IMappingStrategy GetStrategy(MappingAttribute attributeMapping, bool useDefaultIfNonSpecified = true)
        {
            if (!useDefaultIfNonSpecified && attributeMapping.Strategy == null)
                return null;

            var strategyType = attributeMapping.Strategy ?? typeof(DefaultMappingStrategy);

            if (Strategies.TryGetValue(strategyType, out var strategy))
                return strategy;

            strategy = (IMappingStrategy) Activator.CreateInstance(strategyType, this);
            Strategies[strategyType] = strategy;

            return strategy;
        }
    }
}