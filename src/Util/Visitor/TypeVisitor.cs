using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Util.Visitor;

public class TypeVisitor
{
    private readonly IDictionary<int, List<IVisitor>> _orderedActions;
    private readonly IEnumerable<PropertyInfo> _propertiesToVisit;

    public TypeVisitor(Type type, IDictionary<int, List<IVisitor>> orderedActions)
    {
        _orderedActions = orderedActions;

        ShouldVisit = !type.IsPrimitive;

        _propertiesToVisit = type.GetProperties().Where(ShouldVisitProperty);
    }

    public bool ShouldVisit { get; }

    private static bool ShouldVisitProperty(PropertyInfo propertyInfo)
    {
        return !propertyInfo.PropertyType.IsPrimitive && propertyInfo.CanRead && propertyInfo.GetMethod != null;
    }

    public void VisitProperties(object obj, Action<PropertyInfo, object> action)
    {
        foreach (var property in _propertiesToVisit)
        {
            var rawValue = property.GetMethod.Invoke(obj, new object[0]);
            action(property, rawValue);
        }
    }

    public IEnumerable<IVisitor> GetVisitors(int actionOrderIndex)
    {
        return _orderedActions.TryGetValue(actionOrderIndex, out var visitors) ? visitors : Array.Empty<IVisitor>();
    }
}