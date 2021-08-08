using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.Logging;

namespace Util.Visitor
{
    internal class ObjectVisitor : IObjectVisitor
    {
        private readonly ILogger _logger;
        private readonly object _root;
        private List<(VisitPath, object)> _objectsToVisit = null;
        private readonly IDictionary<int, IReadOnlyList<IVisitor>> _actions = new Dictionary<int, IReadOnlyList<IVisitor>>();

        public ObjectVisitor(
            ILogger logger,
            object root,
            IEnumerable<IVisitor> actions)
        {
            _logger = logger;
            _root = root;

            foreach (var grouping in actions.GroupBy(a => a.Order))
                _actions[grouping.Key] = grouping.ToList();
        }

        public void VisitAll()
        {
            _objectsToVisit = new List<(VisitPath, object)>();

            var visited = new List<object>();
            if (_root is IEnumerable enumerable)
            {
                foreach (var obj in enumerable)
                    Visit(new VisitPath(obj), obj, visited);
            }
            else
                Visit(new VisitPath(_root), _root, visited);

            foreach (var actionOrder in _actions.Keys.OrderBy(v => v))
                PerformActions(actionOrder);

            _objectsToVisit = null;
        }

        private void PerformActions(int actionOrder)
        {
            foreach (var action in _actions[actionOrder])
            {
                foreach (var (path, obj) in _objectsToVisit)
                {
                    try
                    {
                        action.Visit(path, obj);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Error performing action {Action} on node: {Path}", action.GetType().FullName, path.ToString());
                    }
                }
            }
        }

        private void Visit(VisitPath path, object obj, List<object> visited)
        {
            if (visited.Contains(obj))
                return;

            visited.Add(obj);

            var type = obj.GetType();

            if (type.IsPrimitive)
                return;

            _objectsToVisit.Add((path, obj));

            var properties = type.GetProperties().Where(ShouldVisitProperty);
            VisitPropertyValues(properties, path, obj, visited);
        }

        private void VisitPropertyValues(IEnumerable<PropertyInfo> properties, VisitPath path, object obj, List<object> visited)
        {
            foreach (var property in properties)
            {
                var rawValue = property.GetMethod.Invoke(obj, new object[0]);
                var propertyName = property.Name;

                object value;
                if (rawValue is IOption option)
                    value = option.HasValue ? option.GetValue() : null;
                else
                    value = rawValue;

                if (visited.Contains(value))
                    return;

                visited.Add(value);

                switch (value)
                {
                    case null:
                        continue;
                    case IDictionary dictionary:
                        var dictionaryPath = path.CreateChild(propertyName, dictionary);
                        VisitDictionary(dictionaryPath, dictionary, visited);
                        break;
                    case IEnumerable enumerable:
                        VisitEnumerableItems(path, enumerable, propertyName, visited);
                        break;
                    default:
                        var nextPath = path.CreateChild(propertyName, value);
                        Visit(nextPath, value, visited);
                        break;
                }
            }
        }

        private void VisitDictionary(VisitPath path, IDictionary dictionary, List<object> visited)
        {
            foreach (var key in dictionary.Keys)
            {
                var element = dictionary[key];
                var childPath = path.CreateChild(key.ToString(), key, element);
                Visit(childPath, element, visited);
            }
        }

        private void VisitEnumerableItems(VisitPath path, IEnumerable enumerable, string propertyName, List<object> visited)
        {
            var index = 0;
            foreach (var element in enumerable)
            {
                var nextPath = path.CreateChild(propertyName, index, element);
                Visit(nextPath, element, visited);

                index++;
            }
        }

        private static bool ShouldVisitProperty(PropertyInfo propertyInfo)
        {
            return !propertyInfo.PropertyType.IsPrimitive && propertyInfo.CanRead && propertyInfo.GetMethod != null;
        }
    }
}