namespace Util.Visitor
{
    public class VisitPath : IVisitPath
    {
        private readonly VisitPath _previous;
        private readonly object _current;
        private readonly string _propertyName;
        private readonly Option<int> _index = Option.None;

        public object Root => _previous?.Root ?? _current;

        public VisitPath(object root)
        {
            _current = root;
        }

        private VisitPath(VisitPath previous, string propertyName, int index, object obj)
        {
            _previous = previous;
            _propertyName = propertyName;
            _index = index;
            _current = obj;
        }

        private VisitPath(VisitPath previous, string propertyName, object value)
        {
            _previous = previous;
            _propertyName = propertyName;
            _current = value;
        }

        public VisitPath CreateChild(string propertyName, int index, object obj)
        {
            return new(this, propertyName, index, obj);
        }

        public VisitPath CreateChild(string propertyName, object value)
        {
            return new(this, propertyName, value);
        }
    }
}