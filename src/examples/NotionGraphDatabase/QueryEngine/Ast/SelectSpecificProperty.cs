namespace NotionGraphDatabase.QueryEngine.Ast;

internal class SelectSpecificProperty : PropertySelector
{
    public string PropertyName { get; }

    public SelectSpecificProperty(Identifier identifier, string propertyName) : base(identifier)
    {
        PropertyName = propertyName;
    }
}