namespace NotionGraphDatabase.Query.Expression;

public interface IPropertyValueResolver
{
    object? GetValue(string alias, string propertyName);
}