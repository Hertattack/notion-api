using System;

namespace NotionGraphDatabase.Integration.Tests.Util;

public class ProxyException : Exception
{
    public string MethodName { get; }

    public ProxyException(string methodName)
    {
        MethodName = methodName;
    }
}