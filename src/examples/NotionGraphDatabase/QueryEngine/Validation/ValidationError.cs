namespace NotionGraphDatabase.QueryEngine.Validation;

public class ValidationError
{
    public ValidationErrorCodes ErrorCode { get; }
    public string Message { get; }

    public ValidationError(ValidationErrorCodes errorCode, string message)
    {
        ErrorCode = errorCode;
        Message = message;
    }
}