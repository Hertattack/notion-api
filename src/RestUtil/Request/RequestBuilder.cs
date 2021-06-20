using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using RestUtil.Request.Attributes;
using Util;

namespace RestUtil.Request
{
    public class RequestBuilder : IRequestBuilder
    {
        private readonly IMapper _mapper;

        public RequestBuilder(IMapper mapper)
        {
            _mapper = mapper;
        }

        public IRequest BuildRequest(object requestDefinition)
        {
            var requestAttributeOption = GetRequestAttribute(requestDefinition);
            if (!requestAttributeOption.HasValue)
                throw new ArgumentException("The object provided is not a request definition.", nameof(requestDefinition));

            var requestAttribute = requestAttributeOption.Value;

            var request = new Request
            {
                Method = requestAttribute.HttpMethod,
                Path = requestAttribute.Path
            };

            var parameters = GetParameters(requestDefinition);

            if (parameters.Count(p => p.Type == ParameterType.Body) > 1)
                throw new ArgumentException("At most one body parameter is supported.", nameof(requestDefinition));

            if (parameters.Count(p => p.Type == ParameterType.Query) > 1)
                throw new ArgumentException("At most one query string parameter is supported.", nameof(requestDefinition));

            foreach (var parameter in parameters)
            {
                var mappedValue = _mapper.Map(parameter);
                if (parameter.Type == ParameterType.Body)
                    request.Body = mappedValue;
                else
                    request.QueryString = mappedValue.ToString();
            }

            return request;
        }

        private static Option<RequestAttribute> GetRequestAttribute(object requestDefinition)
        {
            var type = requestDefinition.GetType();
            return (RequestAttribute) type.GetCustomAttribute(typeof(RequestAttribute));
        }

        private static IReadOnlyList<RequestParameter> GetParameters(object requestDefinition)
        {
            var type = requestDefinition.GetType();

            var parameters = new List<RequestParameter>();

            foreach (var property in type.GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(p => p.GetMethod != null))
            {
                var parameterAttribute = (ParameterAttribute) property.GetCustomAttribute(typeof(ParameterAttribute));
                if (parameterAttribute is null)
                    continue;

                // ReSharper disable once PossibleNullReferenceException
                var propertyValue = property.GetMethod.Invoke(requestDefinition, Array.Empty<object>());
                parameters.Add(new RequestParameter(parameterAttribute.Type, propertyValue) {Strategy = parameterAttribute.Strategy});
            }

            return parameters;
        }
    }
}