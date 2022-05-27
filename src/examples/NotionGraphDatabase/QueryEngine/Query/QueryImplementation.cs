using NotionGraphDatabase.QueryEngine.Query.Path;

namespace NotionGraphDatabase.QueryEngine.Query;

internal class QueryImplementation : IQuery
{
    private Dictionary<NodeReference, List<NodePropertySelection>> _selectedProperties = new();

    private ISet<NodeReference> _nodeReferences = new HashSet<NodeReference>();

    public IEnumerable<NodeReference> NodeReferences =>
        _nodeReferences;

    private List<NodeSelectStepContext> _selectStepsContexts = new();

    public IEnumerable<NodeReturnPropertySelection> ReturnPropertySelections =>
        _selectedProperties.Select(kvp => new NodeReturnPropertySelection(kvp.Key, kvp.Value.AsReadOnly()));

    public NodeReference? FindNodeByAlias(string alias)
    {
        return _nodeReferences.FirstOrDefault(r => r.Alias == alias);
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

    public void AddNextSelectStep(NodeSelectStep nodeSelectStep)
    {
        _nodeReferences.Add(nodeSelectStep.AssociatedNode);
        var nextContext = new NodeSelectStepContext(_selectStepsContexts.LastOrDefault(), nodeSelectStep);
        _selectStepsContexts.Add(nextContext);
    }
}