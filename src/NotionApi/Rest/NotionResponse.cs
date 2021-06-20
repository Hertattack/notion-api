namespace NotionApi.Rest
{
    public class NotionResponse<TResponseType> : INotionResponse<TResponseType>
    {
        public string Object { get; set; }
    }
}