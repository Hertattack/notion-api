using System;
using System.Collections.Generic;
using System.Linq;
using NotionApi.Cache;
using NotionApi.Rest.Response.Database;
using NotionApi.Rest.Response.Database.Properties;
using NotionApi.Rest.Response.Objects;
using NotionApi.Rest.Response.Page;
using NotionApi.Rest.Response.Page.Properties.Relation;

namespace NotionVisualizer.Visualization
{
    public class GraphBuilder
    {
        public Func<PageObject, string, OneToManyRelationPropertyValue, bool> EdgeFilter { get; set; } = (_, __, ___) => true;
        public Func<NotionObject, bool> NodeFilter { get; set; } = _ => true;

        public Graph Build(INotionCache notionCache, IList<NotionObject> notionObjects)
        {
            var graph = new Graph();
            var index = notionObjects.ToDictionary(n => n.Id);

            var nodes = graph.Nodes;
            var edges = graph.Edges;

            foreach (var notionDatabase in notionObjects.OfType<DatabaseObject>())
            {
                if (!NodeFilter(notionDatabase))
                    continue;

                var node = new Node
                {
                    Id = notionDatabase.Id,
                    Type = "database",
                    Name = notionDatabase.Title.HasValue
                        ? string.Join(" ", notionDatabase.Title.Value.Select(t => t.PlainText))
                        : notionDatabase.Id
                };

                nodes.Add(node);
            }

            foreach (var notionPage in notionObjects.OfType<PageObject>())
            {
                if (!NodeFilter(notionPage))
                    continue;

                var type = "page";
                if (notionPage.Container.HasValue 
                    && notionPage.Container.Value is DatabaseObject {Title.HasValue: true} database)
                    type = string.Join(" ", database.Title.Value.Select(t => t.PlainText));
                
                var node = new Node
                {
                    Id = notionPage.Id,
                    Type = type,
                    Name = notionPage.Title,
                    NotionUrl = notionPage.Url
                };

                if (notionPage.Container.HasValue)
                {
                    var container = notionPage.Container.Value;
                    if (container is DatabaseObject databaseContainer && index.ContainsKey(databaseContainer.Id))
                    {
                        node.ParentId = databaseContainer.Id;
                    }
                }

                nodes.Add(node);

                foreach (var (propertyName, property) in notionPage.Properties)
                {
                    if (property is not OneToManyRelationPropertyValue relationPropertyValue)
                        continue;

                    if (!property.Configuration.HasValue || property.Configuration.Value is not RelationPropertyConfiguration relationPropertyConfiguration)
                        continue;

                    var optionalDatabase = notionCache.GetDatabase(relationPropertyConfiguration.Configuration.DatabaseId);

                    if (!optionalDatabase.HasValue)
                        continue;

                    if (!EdgeFilter(notionPage, propertyName, relationPropertyValue))
                        continue;

                    foreach (var relation in relationPropertyValue.Relations)
                    {
                        if (!index.ContainsKey(relation.Id))
                            continue;

                        var edge = new Edge
                        {
                            SourceId = notionPage.Id,
                            TargetId = relation.Id,
                            PropertyName = propertyName
                        };

                        edges.Add(edge);
                    }
                }
            }

            return graph;
        }
    }
}