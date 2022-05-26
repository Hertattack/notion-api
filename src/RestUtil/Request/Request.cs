namespace RestUtil.Request;

internal class Request : IRequest
{
    public object Body { get; set; }
    public string QueryString { get; set; }
    public System.Net.Http.HttpMethod Method { get; set; }
    public string Path { get; set; }
}