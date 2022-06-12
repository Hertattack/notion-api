namespace NotionGraphApi.Interface;

public class DateFieldValue : FieldValue
{
    public string Start { get; }
    public string? End { get; }

    public DateFieldValue(string start, string? end)
    {
        Start = start;
        End = end;
    }
}