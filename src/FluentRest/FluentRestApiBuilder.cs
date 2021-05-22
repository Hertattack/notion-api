using System;
using FluentRest.ApiBuilder;
using FluentRest.DynamicTyping;

namespace FluentRest
{
    public class FluentRestApiBuilder
    {
        private readonly DynamicTypeBuilder _dynamicTypeBuilder;

        public FluentRestApiBuilder()
        {
            _dynamicTypeBuilder = new DynamicTypeBuilder();
        }

        public FluentRestApiBuilder(string forVersion)
        {
            throw new NotImplementedException();
        }

        public FluentRestApiBuilder WithBase(string v1)
        {
            throw new NotImplementedException();
        }

        public IPathBuilder AddPath(string search)
        {
            throw new NotImplementedException();
        }
    }
}