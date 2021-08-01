using System;
using System.Collections.Generic;
using System.Linq;

namespace Util.Visitor
{
    public class ObjectVisitorFactory
    {
        private readonly IList<Action<IVisitPath, object>> _actions = new List<Action<IVisitPath, object>>();

        public void RegisterAction<TVisitedType>(Action<IVisitPath, TVisitedType> action)
        {
            _actions.Add(CreateVisitAction(action));
        }

        private static Action<IVisitPath, object> CreateVisitAction<TVisitedType>(Action<IVisitPath, TVisitedType> action)
        {
            return (path, obj) =>
            {
                if (obj is not TVisitedType objToVisit)
                    return;

                action(path, objToVisit);
            };
        }

        public IObjectVisitor CreateFor(object obj)
        {
            var visitor = new ObjectVisitor(obj, _actions.ToList());

            return visitor;
        }
    }
}