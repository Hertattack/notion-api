namespace NotionGraphDatabase.QueryEngine.Validation;

public enum ValidationErrorCodes
{
    DUPLICATE_ALIASES = 1,
    ALIAS_NOT_DEFINED = 2,
    OPERATOR_NOT_SUPPORTED = 3,
    EXPRESSION_NOT_SUPPORTED = 4
}