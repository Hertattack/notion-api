using System;
using Util;

namespace NotionApi.Rest.Parameter
{
    public class PaginationOptions
    {
        private const int _maxPageSize = 100;

        public Option<string> StartCursor { get; set; }

        private int _pageSize = _maxPageSize;

        public int PageSize
        {
            get => _pageSize;
            set
            {
                if (value <= 0)
                    throw new ArgumentException("Page size must be greater than 0.", nameof(PageSize));

                if (value > _maxPageSize)
                    throw new ArgumentException($"Page size should not exceed the maximum: {_maxPageSize}", nameof(PageSize));

                _pageSize = value;
            }
        }
    }
}