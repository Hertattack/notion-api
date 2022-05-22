using System.Collections.ObjectModel;

namespace NotionGraphDatabase.Query;

public class NodeReturnPropertySelection
{
    public NodeReference NodeReference { get; }
    public ReadOnlyCollection<NodePropertySelection> SelectedProperties { get; }

    public NodeReturnPropertySelection(NodeReference nodeReference,
        ReadOnlyCollection<NodePropertySelection> selectedProperties)
    {
        NodeReference = nodeReference;
        SelectedProperties = selectedProperties;
    }
}