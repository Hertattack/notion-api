using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Extensions.Logging;

namespace Util.Visitor
{
    internal class ObjectVisitor : IObjectVisitor
    {
        private readonly ILogger _logger;
        private readonly object _root;
        private List<(VisitPath, object, TypeVisitor visitor)> _objectsToVisit = null;
        private readonly TypeVisitorCache _typeVisitorCache;

        public ObjectVisitor(
            ILogger logger,
            object root,
            IEnumerable<IVisitor> actions)
        {
            _logger = logger;
            _root = root;

            _typeVisitorCache = new TypeVisitorCache(actions);
        }

        public void VisitAll()
        {
            _objectsToVisit = new List<(VisitPath, object, TypeVisitor)>();

            var visited = new HashSet<object>();
            if (_root is IEnumerable enumerable)
            {
                foreach (var obj in enumerable)
                    Visit(new VisitPath(obj), obj, visited);
            }
            else
                Visit(new VisitPath(_root), _root, visited);

            PerformActions();

            _objectsToVisit = null;
        }

        private void PerformActions()
        {
            foreach (var actionIndex in _typeVisitorCache.ActionOrder)
            {
                foreach (var (path, obj, typeVisitor) in _objectsToVisit)
                {
                    foreach (var visitor in typeVisitor.GetVisitors(actionIndex))
                    {
                        try
                        {
                            visitor.Visit(path, obj);
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError(ex, "Error performing action {Action} on node: {Path}", visitor.GetType().FullName, path.ToString());
                        }
                    }
                }
            }
        }

        private void Visit(VisitPath path, object obj, HashSet<object> visited)
        {
            if (visited.Contains(obj))
                return;

            visited.Add(obj);

            var typeVisitor = _typeVisitorCache.GetVisitor(obj);

            if (!typeVisitor.ShouldVisit)
                return;

            _objectsToVisit.Add((path, obj, typeVisitor));

            typeVisitor.VisitProperties(obj, (p, v) => VisitPropertyValue(p, path, v, visited));
        }

        private void VisitPropertyValue(MemberInfo property, VisitPath path, object rawValue, HashSet<object> visited)
        {
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
                    return;
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

        private void VisitDictionary(VisitPath path, IDictionary dictionary, HashSet<object> visited)
        {
            foreach (var key in dictionary.Keys)
            {
                var element = dictionary[key];
                var childPath = path.CreateChild(key.ToString(), key, element);
                Visit(childPath, element, visited);
            }
        }

        private void VisitEnumerableItems(VisitPath path, IEnumerable enumerable, string propertyName, HashSet<object> visited)
        {
            var index = 0;
            foreach (var element in enumerable)
            {
                var nextPath = path.CreateChild(propertyName, index, element);
                Visit(nextPath, element, visited);

                index++;
            }
        }
    }
}