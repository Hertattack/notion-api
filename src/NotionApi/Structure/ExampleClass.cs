using System;
using FluentRest;
using FluentRest.DynamicTyping;

namespace NotionApi.Structure
{
    public class ExampleClass : IDynamicType, ISearchBody
    {
        private readonly IBodySpecification<ISortParameter> _specification;

        public ExampleClass(IBodySpecification<ISortParameter> spec)
        {
            _specification = spec;
        }
        public ISortParameter Sort
        {
            get => _specification.PropertySpecification<ISortParameter>(nameof(Sort));
            set => throw new Exception("Cannot set property");
        }
    }
}