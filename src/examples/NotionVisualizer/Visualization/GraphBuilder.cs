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
        public Func<PageObject, OneToManyRelationPropertyValue, bool> EdgeFilter { get; set; } = (_, __) => true;

        public Graph Build(INotionCache notionCache, IList<NotionObject> notionObjects)
        {
            var graph = new Graph();
            var index = notionObjects.ToDictionary(n => n.Id);

            var nodes = graph.Nodes;
            var edges = graph.Edges;

            foreach (var notionPage in notionObjects.OfType<DatabaseObject>())
            {
                var node = new Node
                {
                    Id = notionPage.Id,
                    Type = "database"
                };

                nodes.Add(node);
            }

            foreach (var notionPage in notionObjects.OfType<PageObject>())
            {
                var node = new Node
                {
                    Id = notionPage.Id,
                    Type = "page",
                    Name = notionPage.Title
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

                    if (!EdgeFilter(notionPage, relationPropertyValue))
                        continue;

                    foreach (var relation in relationPropertyValue.Relations)
                    {
                        if (!index.ContainsKey(relation.Id))
                            continue;

                        var edge = new Edge
                        {
                            SourceId = notionPage.Id,
                            TargetId = relation.Id
                        };

                        edges.Add(edge);
                    }
                }
            }

            return graph;
        }
    }
}