using Microsoft.Extensions.Logging;

namespace Util.Visitor
{
    public class ObjectVisitorFactory : IObjectVisitorFactory
    {
        private readonly ILoggerFactory _loggerFactory;

        public ObjectVisitorFactory(ILoggerFactory loggerFactory)
        {
            _loggerFactory = loggerFactory;
        }

        public IObjectVisitor CreateFor(object obj, params IVisitor[] visitors)
        {
            var logger = _loggerFactory.CreateLogger(typeof(ObjectVisitor));
            var visitor = new ObjectVisitor(logger, obj, visitors);

            return visitor;
        }
    }
}