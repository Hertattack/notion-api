namespace NotionGraphDatabase.Storage.DataModel;

public class UndefinedPropertyException : Exception
{
    public UndefinedPropertyException(string propertyName) : base($"Property '{propertyName}' has not been defined.")
    {
    }
}