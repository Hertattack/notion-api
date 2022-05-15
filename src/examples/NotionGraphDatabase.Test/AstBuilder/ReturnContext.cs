using System.Collections.Generic;
using System.Linq;
using NotionGraphDatabase.QueryEngine.Model;

namespace NotionGraphDatabase.Test.AstBuilder;

public class ReturnContext : IBuilderContext
{
    private readonly IQueryAstBuilder _queryAstBuilder;

    private readonly List<PropertySelector> Selectors = new();

    public ReturnContext(IQueryAstBuilder queryAstBuilder)
    {
        _queryAstBuilder = queryAstBuilder;
    }

    public ReturnContext AllProperties => this;

    public IQueryAstBuilder From(string nameOrAlias)
    {
        Selectors.Add(new SelectAllProperties(new Identifier(nameOrAlias)));
        return _queryAstBuilder;
    }

    public IQueryAst Build()
    {
        return Selectors.Count switch
        {
            0 => throw new AstBuilderException("No return specified."),
            > 1 => throw new AstBuilderException("Too many property selectors specified."),
            _ => new ReturnSpecification(Selectors.First())
        };
    }
}