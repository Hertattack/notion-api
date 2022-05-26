namespace RestUtil.Request;

public interface IRequestBuilder
{
    IRequest BuildRequest(object requestDefintion);
}