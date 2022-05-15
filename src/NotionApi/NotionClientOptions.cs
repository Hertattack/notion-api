namespace NotionApi;

public class NotionClientOptions
{
    public string BaseUri { get; set; }
    public string Token { get; set; }
    public string ApiVersion { get; set; }
    public int LimitPagesToRetrieve { get; set; }
}