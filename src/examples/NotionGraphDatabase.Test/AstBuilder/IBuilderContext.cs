using NotionGraphDatabase.QueryEngine.Model;

namespace NotionGraphDatabase.Test.AstBuilder;

public interface IBuilderContext
{
    IQueryAst Build();
}