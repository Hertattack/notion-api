using NotionApi.Commands;
using NotionApi.Commands.Builder;

namespace NotionApi
{
    public interface INotionClient
    {
        Version ApiVersion { get; set; }
        ICommandBuilder<T> GetCommandBuilder<T>() where T : ICommand;
    }
}