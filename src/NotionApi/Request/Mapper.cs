using System;
using System.Collections.Generic;
using System.Linq;
using NotionApi.Request.Attributes;
using NotionApi.Request.Mapping;

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

            foreach (var property in type.GetProperties())
            {
                var attributeMapping = (MappingAttribute) property.GetCustomAttributes(mappingAttributeType, inherit: true).FirstOrDefault();

                if (attributeMapping == null)
                    continue;

                var strategy = GetStrategy(attributeMapping);
                values[attributeMapping.Name] = strategy.GetValue(property.GetMethod.Invoke(objectToMap, new object[0]));
            }

            return values;
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
                    return strategy.GetValue(valueToMap);

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