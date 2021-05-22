using System;
using System.Collections.Generic;
using FluentRest.Util;

namespace FluentRest.Commands.Builder
{
    public class VersionedCommandBuilderFactory
    {
        private readonly Version _version;
        private Dictionary<string, QueryParameter> _queryParameters = new Dictionary<string, QueryParameter>();
        private Dictionary<string, IBodyParameter> _bodyParameters = new Dictionary<string, IBodyParameter>();

        public VersionedCommandBuilderFactory(Version version)
        {
            _version = version;
        }

        public string Path { get; set; }
        public HttpMethod Method { get; set; }

        public void AddQueryParameter(string name, bool isRequired, Type type)
        {
            _queryParameters[name] = new QueryParameter(name, isRequired, type);
        }

        public void AddBodyParameter<T>(string parameterName, bool isRequired,
            Func<T, Result> validationFunction = null)
        {
            var bodyParameter = new BodyParameter<T>(isRequired, validationFunction);
            _bodyParameters[parameterName] = bodyParameter;
        }
    }
}