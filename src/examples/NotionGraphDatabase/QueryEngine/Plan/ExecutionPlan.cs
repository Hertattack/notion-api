﻿using NotionGraphDatabase.Metadata;
using NotionGraphDatabase.QueryEngine.Plan.Steps;
using NotionGraphDatabase.QueryEngine.Query;

namespace NotionGraphDatabase.QueryEngine.Plan;

internal class ExecutionPlan : IQueryPlan
{
    private Dictionary<string, Database> Databases { get; }

    private List<IExecutionPlanStep> _steps { get; } = new();

    public IEnumerable<IExecutionPlanStep> Steps =>
        _steps.AsReadOnly();

    public IQuery Query { get; }
    public Metamodel Metamodel { get; }

    public ExecutionPlan(IQuery query, Metamodel metamodel)
    {
        Query = query;
        Metamodel = metamodel;

        Databases = Metamodel.Databases.ToDictionary(d => d.Alias);
    }

    internal void AddStep(IExecutionPlanStep step)
    {
        _steps.Add(step);
    }
}