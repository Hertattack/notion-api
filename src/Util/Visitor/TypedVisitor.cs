using System;

namespace Util.Visitor;

public abstract class TypedVisitor<TVisitType> : IVisitor
{
    public bool WantsToVisit(Type type)
    {
        return typeof(TVisitType).IsAssignableFrom(type);
    }

    public int Order { get; protected set; } = 0;

    public void Visit(VisitPath path, object objToVisit)
    {
        if (objToVisit is not TVisitType obj)
            return;

        Visit(path, obj);
    }

    protected abstract void Visit(VisitPath path, TVisitType obj);
}