using NotionGraphDatabase.Metadata;
using NotionGraphDatabase.Query;

namespace NotionGraphDatabase.QueryEngine.Validation;

public interface IQueryValidator
{
    ValidationResult Validate(IQuery query, Metamodel metamodelStoreMetamodel);
}