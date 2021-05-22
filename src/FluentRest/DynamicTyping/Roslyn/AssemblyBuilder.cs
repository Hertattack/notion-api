using System.Collections.Generic;

namespace FluentRest.DynamicTyping.Roslyn
{
    public class AssemblyBuilder
    {
        private readonly Dictionary<string, IClassBuilder> _classes = new Dictionary<string, IClassBuilder>();

        public AssemblyBuilder()
        {
        }

        public IClassBuilder GetClassBuilder<TForType>(string namespacePrefix, string classPrefix = "D__")
        {
            var type = typeof(TForType);
            var fullName = $"{namespacePrefix}.{type.Namespace}.{classPrefix}{type.Name}";

            if (!_classes.ContainsKey(fullName))
                _classes[fullName] = new ClassBuilder<TForType>(this);

            return _classes[fullName];
        }
    }
}