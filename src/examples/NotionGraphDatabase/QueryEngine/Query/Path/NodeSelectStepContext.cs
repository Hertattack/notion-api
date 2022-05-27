namespace NotionGraphDatabase.QueryEngine.Query.Path;

internal class NodeSelectStepContext : ISelectStepContext
{
    private readonly NodeSelectStepContext? _previousStepContext;

    private readonly NodeSelectStep _currentStep;
    public ISelectStep Step => _currentStep;

    public NodeSelectStepContext(NodeSelectStepContext? previousStep, NodeSelectStep currentStep)
    {
        _previousStepContext = previousStep;
        _currentStep = currentStep;
    }
}