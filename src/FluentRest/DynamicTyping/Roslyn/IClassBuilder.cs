using System.Reflection;

namespace FluentRest.DynamicTyping.Roslyn
{
    public interface IClassBuilder {
        IClassBuilder WithParameter<T>(string spec);
        IClassBuilder Uses<T>();
        IClassBuilder InheritsFrom<T>();
        IClassBuilder Implement(PropertyInfo property, string cSharpCode);
    }
}