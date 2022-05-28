﻿namespace NotionGraphDatabase.QueryEngine.Query.Expression;

public abstract class ExpressionFunction
{
    protected string LeftAlias { get; }
    protected string LeftPropertyName { get; }

    protected ExpressionFunction(string leftAlias, string leftPropertyName)
    {
        LeftAlias = leftAlias;
        LeftPropertyName = leftPropertyName;
    }

    public abstract bool Matches(object value);
}