using Util;

namespace RestUtil.Response;

public interface IResponse<TResult>
{
    Option<TResult> Value { get; }
}