using NotionGraphDatabase.Query.Path;

namespace NotionGraphDatabase.Query;

public interface IQuery
{
    IEnumerable<NodeReturnPropertySelection> ReturnPropertySelections { get; }
    IEnumerable<NodeReference> NodeReferences { get; }

    IEnumerable<ISelectStepContext> SelectSteps { get; }
    void AddNextSelectStep(NodeSelectStep nodeSelectStep);
}