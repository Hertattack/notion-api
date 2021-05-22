using System;
using FluentRest.Util;

namespace FluentRest.Commands.Builder
{
    public class BodyParameter<T> : IBodyParameter
    {
        private readonly Func<T, Result> _validationFunction;

        public BodyParameter(bool isRequired, Func<T, Result> validationFunction)
        {
            _validationFunction = validationFunction;
            throw new System.NotImplementedException();
        }
    }
}