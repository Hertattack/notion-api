namespace FluentRest.Commands.Builder
{
    public interface ICommandBuilder
    {
    }

    public interface ICommandBuilder<T> : ICommandBuilder where T : ICommand
    {
    }
}