using System.Net.Http;

namespace FluentRest.ApiBuilder
{
    public interface IPathBuilder
    {
        IPathOperationBuilder AddOperation(HttpMethod method);
    }
}