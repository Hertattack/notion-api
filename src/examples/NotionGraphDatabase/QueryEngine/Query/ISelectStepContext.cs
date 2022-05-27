using NotionGraphDatabase.QueryEngine.Query.Path;

namespace NotionGraphDatabase.QueryEngine.Query;

public interface ISelectStepContext
{
    ISelectStep Step { get; }
}