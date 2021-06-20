using Util;

namespace NotionApi
{
    public interface INotionResponse<TResult>
    {
        Option<TResult> Result { get; }
    }
}