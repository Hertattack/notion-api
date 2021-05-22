using System;
using FluentRest.DynamicTyping;

namespace FluentRest.ApiBuilder
{
    public class PathOperationBuilder : IPathOperationBuilder
    {
        internal DynamicTypeBuilder DynamicTypeBuilder { get; }

        public PathOperationBuilder(DynamicTypeBuilder dynamicTypeBuilder)
        {
            DynamicTypeBuilder = dynamicTypeBuilder;
        }

        public TBodyType AddBodySpecification<TBodyType>()
        {
            if (!typeof(TBodyType).IsInterface)
                throw new Exception("Can only support interfaces.");

            // var type = DynamicTypeBuilder.BuildDefinition<TBodyType>();
            //
            // var spec = new BodySpecification<TBodyType>(this);
            //
            // var ctor = type.GetConstructor(new[] {typeof(IBodySpecification<TBodyType>)});
            //
            // return (TBodyType) ctor.Invoke(new[] {spec});

            return default;
        }
    }
}