﻿namespace NotionGraphDatabase.Query;

public class InvalidQueryException : Exception
{
    public InvalidQueryException(string message) : base(message)
    {
    }
}