namespace NotionGraphDatabase.Interface.Analysis;

public class StepDescription
{
    public int Order { get; }
    public string Description { get; }

    public StepDescription(int order, string description)
    {
        Order = order;
        Description = description;
    }
}