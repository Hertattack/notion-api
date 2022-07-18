using NotionApi.Extensions;
using NotionApi.Rest.Response.Database;
using NotionApi.Rest.Response.Database.Properties;
using NotionGraphDatabase.Util;

namespace NotionGraphDatabase.Storage.DataModel;

public class DatabaseDefinition
{
    public string Id { get; }

    public string Title { get; }

    private readonly List<PropertyDefinition> _properties;

    public IEnumerable<PropertyDefinition> Properties =>
        _properties.AsReadOnly();

    public DatabaseDefinition(string databaseId, DatabaseObject notionRepresentation)
    {
        Id = databaseId.AddDashes();

        Title = notionRepresentation.Title.ToPlainTextString();

        _properties = notionRepresentation.Properties
            .Select(kvp => CreatePropertyDefinition(kvp.Key, kvp.Value))
            .ToList();
    }

    private static PropertyDefinition CreatePropertyDefinition(string propertyName,
        NotionPropertyConfiguration configuration)
    {
        return configuration switch
        {
            FormulaPropertyConfiguration => new PropertyDefinition(propertyName, configuration.Type),
            NumberPropertyConfiguration => new PropertyDefinition(propertyName, configuration.Type),
            RelationPropertyConfiguration => new PropertyDefinition(propertyName, configuration.Type),
            RollupPropertyConfiguration => new PropertyDefinition(propertyName, configuration.Type),
            SelectPropertyConfiguration => new PropertyDefinition(propertyName, configuration.Type),
            MultiSelectPropertyConfiguration => new PropertyDefinition(propertyName, configuration.Type),
            _ => new PropertyDefinition(propertyName, configuration.Type)
        };
    }

    public PropertyDefinition GetProperty(string propertyName)
    {
        return _properties.First(p => p.Name == propertyName);
    }
}