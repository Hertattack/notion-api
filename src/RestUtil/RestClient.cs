namespace RestUtil
{
    public class RestClient : IRestClient
    {
        private readonly RestSharp.IRestClient implementation;
    }
}