namespace Util.Visitor
{
    public interface IVisitor
    {
        void Visit(VisitPath path, object objToVisit);
    }
}