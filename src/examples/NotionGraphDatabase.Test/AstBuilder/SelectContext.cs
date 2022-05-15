using System;
using NotionGraphDatabase.QueryEngine.Model;

namespace NotionGraphDatabase.Test.AstBuilder;

public class SelectContext : IBuilderContext
{
    private readonly IQueryAstBuilder _queryAstBuilder;
    private string? _nodeName;

    public bool HasContents => _nodeName != null;

    public SelectContext(IQueryAstBuilder queryAstBuilder)
    {
        _queryAstBuilder = queryAstBuilder;
    }

    public IQueryAstBuilder Node(string nodeName)
    {
        if (_nodeName != null)
            throw new Exception($"A node has already been selected with that name: {nodeName}");

        _nodeName = nodeName;
        return _queryAstBuilder;
    }

    public IQueryAst Build()
    {
        if (_nodeName == null)
            throw new AstBuilderException("Cannot build select expression, no node selected.");

        return new NodeClassReference(new Identifier(_nodeName));
    }
}