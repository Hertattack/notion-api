﻿using NotionGraphDatabase.QueryEngine.Model;
using NotionGraphDatabase.QueryEngine.Query;

namespace NotionGraphDatabase.QueryEngine;

public interface IQueryEngine
{
    IQuery Parse(string queryText);
}