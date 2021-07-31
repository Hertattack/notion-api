using System.Collections.Generic;

namespace NotionApi.Rest.Common
{
    public interface IPaginatedResponse<TResultType>
    {
        bool HasMore { get; set; }
        string NextCursor { get; set; }
        string ObjectType { get; set; }
        IList<TResultType> Results { get; set; }
    }
}