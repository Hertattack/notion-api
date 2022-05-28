using System;

namespace NotionGraphDatabase.Test.Util;

public class NullDisposable : IDisposable
{
    public void Dispose()
    {
    }
}