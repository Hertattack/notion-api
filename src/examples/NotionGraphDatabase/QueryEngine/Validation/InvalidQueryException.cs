namespace NotionGraphDatabase.QueryEngine.Validation;

public class InvalidQueryException : Exception
{
    public ValidationResult ValidationResult { get; }

    public InvalidQueryException(ValidationResult validationResult) : base("Query is invalid.")
    {
        ValidationResult = validationResult;
    }
}