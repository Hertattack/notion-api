namespace NotionGraphDatabase.Query.Filter;

public class ComparisonOperator
{
    public bool IsNegated { get; }

    public ComparisonType Type { get; }

    public ComparisonOperator(ComparisonType type, bool isNegated)
    {
        IsNegated = isNegated;
        Type = type;
    }

    public override string ToString()
    {
        var negationText = IsNegated ? "! " : "";
        return negationText + Type switch
        {
            ComparisonType.EQUALS => "=",
            ComparisonType.CONTAINS => "~=",
            ComparisonType.STARTS_WITH => "?=",
            ComparisonType.ENDS_WITH => "=?",
            ComparisonType.GREATER_THAN => ">",
            ComparisonType.LESS_THAN => "<",
            ComparisonType.GREATER_OR_EQUAL => ">=",
            ComparisonType.LESS_OR_EQUAL => "<=",
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}