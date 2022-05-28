namespace NotionGraphDatabase.QueryEngine.Validation;

public class ValidationResult
{
    public bool IsInvalid { get; }

    internal ValidationResult()
    {
        
    }
    
    internal void AddError(ValidationError validationError)
    {
    }
}