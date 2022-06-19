using NotionGraphDatabase.Query.Path;

namespace NotionGraphDatabase.Query;

public interface ISelectStepContext
{
    ISelectStep Step { get; }
    ISelectStepContext? PreviousStepContext { get; }
    IEnumerable<ISelectStepContext> Path { get; }
}