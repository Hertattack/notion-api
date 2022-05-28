using NotionGraphDatabase.QueryEngine.Validation;

namespace NotionGraphDatabase.QueryEngine;

public class InvalidQueryException : Exception
{
    public ValidationResult ValidationResult { get; }

    public InvalidQueryException(ValidationResult validationResult) : base("Query is invalid.")
    {
        ValidationResult = validationResult;
    }
}