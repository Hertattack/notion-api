using NotionGraphDatabase.QueryEngine.Query.Path;

namespace NotionGraphDatabase.QueryEngine.Query;

internal class QueryImplementation : IQuery
{
    private Dictionary<NodeReference, List<NodePropertySelection>> _selectedProperties = new();

    public IEnumerable<NodeReturnPropertySelection> ReturnPropertySelections =>
        _selectedProperties.Select(kvp => new NodeReturnPropertySelection(kvp.Key, kvp.Value.AsReadOnly()));

    public IEnumerable<NodeReference> NodeReferences =>
        SelectionPath.Aliases.Values.Select(s => s.AssociatedNode).Distinct();

    public QueryImplementation(QueryPath queryPath)
    {
        SelectionPath = queryPath;
    }

    public QueryPath SelectionPath { get; }

    public NodeReference? FindNodeByAlias(string alias)
    {
        return !SelectionPath.Aliases.TryGetValue(alias, out var pathStep) ? null : pathStep.AssociatedNode;
    }

    public void AddPropertySelection(NodePropertySelection propertySelection)
    {
        if (_selectedProperties.TryGetValue(propertySelection.ReferencedNode, out var selectedProperties))
        {
            if (selectedProperties.Any(s => s.MatchesOrExtends(propertySelection)))
                return;

            selectedProperties.Add(propertySelection);
        }
        else
        {
            _selectedProperties.Add(propertySelection.ReferencedNode,
                new List<NodePropertySelection> {propertySelection});
        }
    }
}