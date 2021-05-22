using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.CodeAnalysis.CSharp;

namespace FluentRest.DynamicTyping.Roslyn
{
    public class ClassBuilder<TForType> : IClassBuilder
    {
        private readonly AssemblyBuilder _assemblyBuilder;

        private readonly Dictionary<string, Type> _parameters = new();

        private readonly ISet<string> _usings = new HashSet<string>();

        private readonly ISet<Type> _inheritsFrom = new HashSet<Type>();

        public ClassBuilder(AssemblyBuilder assemblyBuilder)
        {
            _assemblyBuilder = assemblyBuilder;
        }

        public IClassBuilder WithParameter<T>(string spec)
        {
            _parameters[spec] = typeof(T);
            return this;
        }

        public IClassBuilder Uses<T>()
        {
            _usings.Add(typeof(T).Namespace ?? throw new InvalidOperationException());
            return this;
        }

        public IClassBuilder InheritsFrom<T>()
        {
            _inheritsFrom.Add(typeof(T));
            return Uses<T>();
        }

        public IClassBuilder Implement(PropertyInfo property, string cSharpCode)
        {
            var propertyImplementation = new PropertyImplmentation(property.Name, cSharpCode);
            
        }
    }
}