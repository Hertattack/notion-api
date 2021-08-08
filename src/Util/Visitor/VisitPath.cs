using System.Collections.Generic;
using System.Text;

namespace Util.Visitor
{
    public class VisitPath : IVisitPath
    {
        private readonly Option<VisitPath> _previous;
        private readonly Option<string> _propertyName;
        private readonly Option<int> _index = Option.None;
        private readonly Option<object> _key = Option.None;

        public VisitPath Root => _previous.HasValue ? _previous.Value : this;

        private IReadOnlyList<VisitPath> _fullPath;

        public IReadOnlyList<VisitPath> FullPath
        {
            get
            {
                if (_fullPath == null)
                {
                    var result = new List<VisitPath>();
                    var pointer = this;
                    result.Add(pointer);
                    while (pointer._previous.HasValue)
                    {
                        pointer = pointer._previous.Value;
                        result.Add(pointer);
                    }

                    result.Reverse();
                    _fullPath = result;
                }

                return _fullPath;
            }
        }

        public Option<VisitPath> Previous =>
            _previous;

        public object Target { get; }

        public VisitPath(object root)
        {
            Target = root;
        }

        private VisitPath(VisitPath previous, string propertyName, object key, object obj)
        {
            _previous = previous;
            _propertyName = propertyName;

            if (key is int index)
                _index = index;
            else
                _key = key;

            Target = obj;
        }

        private VisitPath(VisitPath previous, string propertyName, object value)
        {
            _previous = previous;
            _propertyName = propertyName;
            Target = value;
        }

        public VisitPath CreateChild(string propertyName, int index, object obj)
        {
            return new(this, propertyName, index, obj);
        }

        public VisitPath CreateChild(string propertyName, object key, object obj)
        {
            return new(this, propertyName, key, obj);
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
                if (step._propertyName.HasValue)
                    stringBuilder.Append($" [{step._propertyName.Value}]");

                if (stringBuilder.Length > 0)
                    stringBuilder.Append(" > ");

                stringBuilder.Append(step.Target);
            }

            return stringBuilder.ToString();
        }

        public Option<T> FindPrevious<T>()
        {
            var current = this;
            
            while (current.Previous.HasValue)
            {
                var previous = current.Previous.Value;
                
                if (previous.Target is T t)
                    return t;
            
                current = previous;
            }

            return default;
        }
    }
}