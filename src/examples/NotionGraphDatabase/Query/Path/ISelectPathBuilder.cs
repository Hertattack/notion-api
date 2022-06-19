using NotionGraphDatabase.Query.Parser.Ast;

namespace NotionGraphDatabase.Query.Path;

internal interface ISelectPathBuilder
{
    void FromAst(IQuery query, SelectExpression selectExpression);
}