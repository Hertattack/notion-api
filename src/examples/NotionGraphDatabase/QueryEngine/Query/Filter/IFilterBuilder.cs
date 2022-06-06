using NotionGraphDatabase.QueryEngine.Ast;

namespace NotionGraphDatabase.QueryEngine.Query.Filter;

internal interface IFilterBuilder
{
    IEnumerable<FilterExpression> FromAst(IQuery query, NodeClassReference nodeClassReference);
}