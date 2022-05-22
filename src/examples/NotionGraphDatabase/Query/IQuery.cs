namespace NotionGraphDatabase.Query;

public interface IQuery
{
    IEnumerable<NodeReturnPropertySelection> ReturnPropertySelections { get; }
}