using System;
using System.Collections.Generic;
using System.Linq;
using NotionApi.Cache;
using NotionApi.Rest.Database;
using NotionApi.Rest.Database.Properties;
using NotionApi.Rest.Objects;
using NotionApi.Rest.Page;
using NotionApi.Rest.Page.Properties.Relation;

namespace NotionVisualizer.Generator.Graph
{
    public class GraphGenerator
    {
        private readonly string _nodeSource;
        private readonly string _tagDatabase;
        private readonly bool _setParent;

        public GraphGenerator(string nodeSource, string tagDatabase, bool setParent)
        {
            _nodeSource = nodeSource;
            _tagDatabase = tagDatabase;
            _setParent = setParent;
        }

        public IEnumerable<object> GetGraphData(INotionCache notionCache, IList<NotionObject> notionObjects)
        {
            var index = notionObjects.ToDictionary(n => n.Id);

            var nodes = new List<Node>();
            var edges = new List<Edge>();

            Func<PageObject, bool> nodeFilter = _ => true;

            if (!string.IsNullOrEmpty(_nodeSource))
                nodeFilter = FilterOnSource;

            Func<DatabaseObject, bool> databaseFilter = _ => true;
            if (!string.IsNullOrEmpty(_tagDatabase))
                databaseFilter = FilterTagDatabase;

            foreach (var notionPage in notionObjects.OfType<DatabaseObject>().Where(databaseFilter))
            {
                var node = new Node
                {
                    Id = notionPage.Id,
                    Classes = { "database" }
                };

                nodes.Add(node);
            }

            foreach (var notionPage in notionObjects.OfType<PageObject>().Where(nodeFilter))
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

                    if (!optionalDatabase.HasValue || !databaseFilter(optionalDatabase.Value))
                        continue;

                    if (!databaseFilter(property.Configuration.Value.Container.Value))
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

            return nodes.Cast<object>().Concat(edges);
        }

        private bool FilterTagDatabase(DatabaseObject databaseObject)
        {
            var databaseId = databaseObject.Id.Replace("-", "").ToLowerInvariant();

            if (!string.IsNullOrEmpty(_nodeSource) && databaseId.Equals(_nodeSource.ToLowerInvariant()))
                return true;

            return databaseId.Equals(_tagDatabase.ToLowerInvariant());
        }

        private bool FilterOnSource(PageObject pageObject)
        {
            if (!pageObject.Container.HasValue)
                return false;

            var containerId = pageObject.Container.Value.Id.Replace("-", "").ToLowerInvariant();

            if (containerId.Equals(_nodeSource.ToLowerInvariant()))
                return true;

            if (!string.IsNullOrEmpty(_tagDatabase))
                return containerId.Equals(_tagDatabase.ToLowerInvariant());

            return false;
        }
    }
}