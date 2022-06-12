using System.Collections;

namespace NotionGraphApi.Interface;

public class ListFieldValue : FieldValue, IEnumerable<FieldValue>
{
    private List<FieldValue> _backingList = new();

    public void Add(FieldValue value)
    {
        _backingList.Add(value);
    }

    public IEnumerator<FieldValue> GetEnumerator()
    {
        return _backingList.AsReadOnly().GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}