namespace Util.Visitor
{
    public interface IVisitor
    {
        int Order { get; }
        void Visit(VisitPath path, object objToVisit);
    }
}