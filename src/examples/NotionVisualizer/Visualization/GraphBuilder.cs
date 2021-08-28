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
        private readonly bool _setParent;

        public GraphBuilder(bool setParent)
        {
            _setParent = setParent;
        }

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
                    Classes = { "database" }
                };

                nodes.Add(node);
            }

            foreach (var notionPage in notionObjects.OfType<PageObject>())
            {
                var node = new Node
                {
                    Id = notionPage.Id,
                    Classes = { "page" },
                    Name = notionPage.Title
                };

                if (notionPage.Container.HasValue && _setParent)
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