using System.Text;
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

    public override string ToString()
    {
        var stringBuilder = new StringBuilder();

        var isFirstStep = true;
        foreach (var selectStepContext in _selectStepsContexts)
        {
            var node = selectStepContext.Step.AssociatedNode;
            if (!isFirstStep)
                stringBuilder.Append($"-[{selectStepContext.Step.Role}]->");

            stringBuilder.Append('(');

            if (node.Alias != node.NodeName)
                stringBuilder.Append($"{node.Alias}:");

            stringBuilder.Append($"{node.NodeName})");
            isFirstStep = false;
        }

        stringBuilder.Append(Environment.NewLine);
        stringBuilder.Append(" return ");
        var separator = "";
        foreach (var returnPropertySelection in ReturnPropertySelections)
        {
            var selection = returnPropertySelection.PropertySelection;
            switch (selection)
            {
                case NodeAllPropertiesSelected:
                    stringBuilder.Append($"{separator}{returnPropertySelection.NodeReference.Alias}.*");
                    break;
                case NodeSpecificPropertiesSelected specificPropertiesSelected:
                    foreach (var propertyName in specificPropertiesSelected.PropertyNames)
                        stringBuilder.Append(
                            $"{separator}{returnPropertySelection.NodeReference.Alias}.'{propertyName}'");
                    break;
                default:
                    throw new Exception(
                        $"Missing implementation for converting query to string. Missing type: {selection.GetType().FullName}");
            }

            separator = ", ";
        }

        return stringBuilder.ToString();
    }
}