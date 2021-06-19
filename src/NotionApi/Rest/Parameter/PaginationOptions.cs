using System;
using Util;

namespace NotionApi.Rest.Parameter
{
    public class PaginationOptions
    {
        private const int MaxPageSize = 100;

        public Option<string> StartCursor { get; set; }

        private int pageSize = MaxPageSize;

        public int PageSize
        {
            get => pageSize;
            set
            {
                if (value <= 0)
                    throw new ArgumentException("Page size must be greater than 0.", nameof(PageSize));

                if (value > MaxPageSize)
                    throw new ArgumentException($"Page size should not exceed the maximum: {MaxPageSize}", nameof(PageSize));

                pageSize = value;
            }
        }
    }
}