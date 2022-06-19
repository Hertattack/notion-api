using NotionGraphDatabase.Query.Parser.Ast;

namespace NotionGraphDatabase.Query.Filter;

internal interface IFilterBuilder
{
    IEnumerable<FilterExpression> FromAst(IQuery query, NodeClassReference nodeClassReference);
}