namespace NotionApi
{
    public interface INotionClient
    {
        Version ApiVersion { get; set; }
        //ICommandBuilder<T> GetCommandBuilder<T>() where T : ICommand;
    }
}