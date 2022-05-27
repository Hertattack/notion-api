using NotionGraphDatabase.QueryEngine.Query.Path;

namespace NotionGraphDatabase.QueryEngine.Query;

public interface IQuery
{
    IEnumerable<NodeReturnPropertySelection> ReturnPropertySelections { get; }
    IEnumerable<NodeReference> NodeReferences { get; }

    IEnumerable<ISelectStepContext> SelectSteps { get; }
    void AddNextSelectStep(NodeSelectStep nodeSelectStep);
}