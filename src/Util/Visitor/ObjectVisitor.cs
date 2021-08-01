using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Util.Visitor
{
    internal class ObjectVisitor : IObjectVisitor
    {
        private readonly IReadOnlyList<Action<VisitPath, object>> _actions;
        private readonly object _root;

        public ObjectVisitor(object root, IReadOnlyList<Action<VisitPath, object>> actions)
        {
            _actions = actions;
            _root = root;
        }

        public void VisitAll()
        {
            var path = new VisitPath(_root);
            Visit(new VisitPath(_root), _root);
        }

        private void Visit(VisitPath path, object obj)
        {
            var type = obj.GetType();

            if (type.IsPrimitive)
                return;

            foreach (var action in _actions)
                action(path, obj);

            foreach (var property in type.GetProperties().Where(ShouldVisitProperty))
            {
                var value = property.GetValue(_root);

                if (value is IEnumerable enumerable)
                {
                    var index = 0;
                    foreach (var element in enumerable)
                    {
                        var nextPath = path.CreateChild(property.Name, index, element);
                        Visit(nextPath, element);

                        index++;
                    }
                }
                else
                {
                    var nextPath = path.CreateChild(property.Name, value);
                    Visit(nextPath, value);
                }
            }
        }

        private bool ShouldVisitProperty(PropertyInfo propertyInfo)
        {
            return !propertyInfo.PropertyType.IsPrimitive && propertyInfo.CanRead && propertyInfo.GetMethod != null;
        }
    }
}