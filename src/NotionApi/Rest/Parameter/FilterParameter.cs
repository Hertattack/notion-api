﻿using NotionApi.Request.Attributes;
using NotionApi.Request.Mapping;

namespace NotionApi.Rest.Parameter
{
    [Mapping("filter", Strategy = typeof(ToNestedObjectStrategy))]
    public class FilterParameter
    {
        public FilterType Value { get; set; } = FilterType.None;

        public FilterProperty Property { get; set; } = FilterProperty.Page;
    }
}