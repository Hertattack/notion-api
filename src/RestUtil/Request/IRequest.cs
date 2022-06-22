namespace RestUtil.Request;

public interface IRequest
{
    object? Body { get; }
    string QueryString { get; }
    System.Net.Http.HttpMethod Method { get; }
    string Path { get; }
}