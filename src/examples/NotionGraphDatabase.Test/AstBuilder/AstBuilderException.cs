using System;

namespace NotionGraphDatabase.Test.AstBuilder;

public class AstBuilderException : Exception
{
    public AstBuilderException(string message) : base(message)
    {
    }
}