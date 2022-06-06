﻿using NotionGraphDatabase.QueryEngine.Execution;
using NotionGraphDatabase.Storage;

namespace NotionGraphDatabase.QueryEngine.Plan;

internal abstract class ExecutionPlanStep
{
    public abstract void Execute(QueryExecutionContext context, IStorageBackend storageBackend);
}