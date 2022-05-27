using NotionGraphDatabase.QueryEngine.Model;

namespace NotionGraphDatabase.QueryEngine.Query.Filter;

internal interface IFilterBuilder
{
    IEnumerable<FilterExpression> FromAst(IQuery query, NodeClassReference nodeClassReference);
}