using NotionGraphDatabase.QueryEngine.Query.Path;

namespace NotionGraphDatabase.QueryEngine.Query;

internal class QueryImplementation : IQuery
{
    private Dictionary<NodeReference, NodePropertySelection> _selectedProperties = new();

    private ISet<NodeReference> _nodeReferences = new HashSet<NodeReference>();

    public IEnumerable<NodeReference> NodeReferences =>
        _nodeReferences;

    private List<NodeSelectStepContext> _selectStepsContexts = new();
    public IEnumerable<ISelectStepContext> SelectSteps => _selectStepsContexts.AsReadOnly();

    public IEnumerable<NodeReturnPropertySelection> ReturnPropertySelections =>
        _selectedProperties.Select(kvp =>
            new NodeReturnPropertySelection(kvp.Key, kvp.Value));

    public NodeReference? FindNodeByAlias(string alias)
    {
        return _nodeReferences.FirstOrDefault(r => r.Alias == alias);
    }

    public void SetPropertySelection(NodePropertySelection propertySelection)
    {
        var nodeReference = propertySelection.ReferencedNode;
        _selectedProperties[nodeReference] = propertySelection;
    }

    public void AddNextSelectStep(NodeSelectStep nodeSelectStep)
    {
        _nodeReferences.Add(nodeSelectStep.AssociatedNode);
        var nextContext = new NodeSelectStepContext(_selectStepsContexts.LastOrDefault(), nodeSelectStep);
        _selectStepsContexts.Add(nextContext);
    }
}