using NotionGraphDatabase.QueryEngine.Model;

namespace NotionGraphDatabase.Test.AstBuilder;

public interface IQueryAstBuilder : IBuilderContext
{
    SelectContext Selecting { get; }
    ReturnContext Returning { get; }
}