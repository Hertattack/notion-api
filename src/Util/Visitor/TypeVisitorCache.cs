using System;
using System.Collections.Generic;
using System.Linq;

namespace Util.Visitor;

public class TypeVisitorCache
{
    private IDictionary<Type, TypeVisitor> _cache = new Dictionary<Type, TypeVisitor>();
    private readonly IReadOnlyList<IGrouping<int, IVisitor>> _actionGrouping;

    public IEnumerable<int> ActionOrder => _actionGrouping.Select(g => g.Key).OrderBy(k => k);

    public TypeVisitorCache(IEnumerable<IVisitor> actions)
    {
        _actionGrouping = actions.GroupBy(a => a.Order).ToList();
    }

    public TypeVisitor GetVisitor(object obj)
    {
        var type = obj.GetType();
        if (_cache.TryGetValue(type, out var visitor))
            return visitor;

        var relevantActions = _actionGrouping
            .Where(g => g.Any(v => v.WantsToVisit(type)))
            .ToDictionary(g => g.Key, g => g.Select(v => v).ToList());

        visitor = new TypeVisitor(type, relevantActions);

        _cache[type] = visitor;

        return visitor;
    }
}