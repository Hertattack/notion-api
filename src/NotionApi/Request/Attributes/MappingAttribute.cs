﻿using System;
using NotionApi.Request.Mapping;

namespace NotionApi.Request.Attributes
{
    [AttributeUsage(AttributeTargets.All, Inherited = true, AllowMultiple = false)]
    public class MappingAttribute : Attribute
    {
        public MappingAttribute()
        {
            Name = "";
        }

        public MappingAttribute(string name)
        {
            Name = name;
        }

        public string Name { get; }

        private Type _strategy;

        public Type Strategy
        {
            get => _strategy;
            set => _strategy = typeof(IMappingStrategy).IsAssignableFrom(value)
                ? _strategy = value
                : throw new ArgumentException($"Cannot use type: {value?.FullName}, it does not implement the '{nameof(IMappingStrategy)}' interface");
        }
    }
}