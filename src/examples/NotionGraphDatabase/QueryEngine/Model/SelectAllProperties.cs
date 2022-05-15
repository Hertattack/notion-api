namespace NotionGraphDatabase.QueryEngine.Model;

internal class SelectAllProperties : PropertySelector
{
    public SelectAllProperties(Identifier identifier)
    {
        NodeTypeIdentifier = identifier;
    }
}