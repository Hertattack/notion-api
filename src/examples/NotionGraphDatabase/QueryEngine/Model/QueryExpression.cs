﻿namespace NotionGraphDatabase.QueryEngine.Model;

internal class QueryExpression : QueryPredicate
{
    public SelectExpression SelectExpression { get; }
    public ReturnSpecification ReturnSpecification { get; }

    public QueryExpression(SelectExpression selectExpression, ReturnSpecification returnSpecification)
    {
        SelectExpression = selectExpression;
        ReturnSpecification = returnSpecification;
    }
}