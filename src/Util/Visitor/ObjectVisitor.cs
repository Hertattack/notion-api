using System;
using System.Collections.Generic;

namespace Util.Visitor
{
    public class ObjectVisitor
    {
        private Dictionary<Type, Action<VisitorPath, object>> Actions = new Dictionary<Type, Action<VisitorPath, object>>();

        public void RegisterAction<TVisitedType>(Action<VisitorPath, TVisitedType> action)
        {
        }
    }
}