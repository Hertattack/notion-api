namespace Util.Visitor
{
    public interface IObjectVisitorFactory
    {
        IObjectVisitor CreateFor(object obj, params IVisitor[] visitors);
    }
}