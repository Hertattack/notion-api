using System.Text;

namespace NotionGraphDatabase.QueryEngine.Query.Path;

internal class NodeSelectStepContext : ISelectStepContext
{
    public ISelectStepContext? PreviousStepContext { get; }

    private readonly NodeSelectStep _currentStep;
    public ISelectStep Step => _currentStep;

    private readonly List<NodeSelectStepContext> _path;
    public IEnumerable<ISelectStepContext> Path => _path.AsReadOnly();

    public NodeSelectStepContext(NodeSelectStepContext? previousStep, NodeSelectStep currentStep)
    {
        PreviousStepContext = previousStep;
        _currentStep = currentStep;

        _path = previousStep is null
            ? new List<NodeSelectStepContext>()
            : new List<NodeSelectStepContext>(previousStep._path);

        _path.Add(this);
    }

    public string ToPathString()
    {
        var sb = new StringBuilder();
        var isFirst = true;
        foreach (var pathElement in Path)
        {
            if (!isFirst)
                sb.Append($"-[{pathElement.Step.Role}]->");

            var currentStep = pathElement.Step;
            var associatedNode = currentStep.AssociatedNode;
            if (associatedNode.Alias != associatedNode.NodeName)
                sb.Append($"({associatedNode.Alias}:{associatedNode.NodeName})");

            isFirst = false;
        }

        return sb.ToString();
    }
}