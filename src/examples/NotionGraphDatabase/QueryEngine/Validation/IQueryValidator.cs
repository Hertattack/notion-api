using NotionGraphDatabase.Metadata;
using NotionGraphDatabase.QueryEngine.Query;

namespace NotionGraphDatabase.QueryEngine.Validation;

public interface IQueryValidator
{
    ValidationResult Validate(IQuery query, Metamodel metamodelStoreMetamodel);
}