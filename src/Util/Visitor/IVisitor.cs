using System;

namespace Util.Visitor
{
    public interface IVisitor
    {
        bool WantsToVisit(Type type);
        int Order { get; }
        void Visit(VisitPath path, object objToVisit);
    }
}