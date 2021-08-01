namespace Util.Visitor
{
    public abstract class TypedVisitor<TVisitType> : IVisitor
    {
        public void Visit(VisitPath path, object objToVisit)
        {
            if (objToVisit is not TVisitType obj)
                return;

            Visit(path, obj);
        }

        protected abstract void Visit(VisitPath path, TVisitType obj);
    }
}