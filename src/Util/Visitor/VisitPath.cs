using System.Collections.Generic;
using System.Text;

namespace Util.Visitor
{
    public class VisitPath : IVisitPath
    {
        private readonly VisitPath _previous;
        private readonly object _current;
        private readonly Option<string> _propertyName;
        private readonly Option<int> _index = Option.None;

        public object Root => _previous?.Root ?? _current;

        private IReadOnlyList<VisitPath> _fullPath;

        public IReadOnlyList<VisitPath> FullPath
        {
            get
            {
                if (_fullPath == null)
                {
                    var result = new List<VisitPath>();
                    var pointer = this;
                    do
                    {
                        result.Add(pointer);
                        pointer = pointer._previous;
                    } while (pointer is not null);

                    result.Reverse();
                    _fullPath = result;
                }

                return _fullPath;
            }
        }

        public Option<VisitPath> Previous =>
            _previous;

        public object Target => _current;


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

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();
            foreach (var step in FullPath)
            {
                if (!step._propertyName.HasValue)
                    continue;

                stringBuilder.Append(_previous._current);
                stringBuilder.Append($" [{step._propertyName.Value}> ");
            }

            return stringBuilder.ToString();
        }
    }
}