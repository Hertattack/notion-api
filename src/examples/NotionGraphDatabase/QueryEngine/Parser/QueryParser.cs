using NotionGraphDatabase.QueryEngine.Model;
using sly.lexer;
using sly.parser.generator;

namespace NotionGraphDatabase.QueryEngine.Parser;

internal class QueryParser
{
    [Production("query: selectExpression")]
    public IQueryAst QueryExpression(SelectExpression selectExpression)
    {
        var returnSpecification = new ReturnAllSpecification();
        return new QueryAbstractSyntaxTree(selectExpression, returnSpecification);
    }

    [Production("query: selectExpression returnSpecification")]
    public IQueryAst QueryExpression(SelectExpression selectExpression, ReturnSpecification returnSpecification)
    {
        return new QueryAbstractSyntaxTree(selectExpression, returnSpecification);
    }

    [Production("returnSpecification: RETURN propertySelector")]
    public IQueryAst ReturnSpecification(Token<QueryToken> returnStatement, PropertySelector selector)
    {
        return new ReturnSpecification(selector);
    }

    [Production("propertySelector: identifier OBJECT_ACCESS ALL_PROPERTIES")]
    public IQueryAst PropertySelector(Identifier identifier, Token<QueryToken> objectAccessToken,
        Token<QueryToken> allPropertiesToken)
    {
        return new SelectAllProperties(identifier);
    }

    [Production("selectExpression: nodeClassReference")]
    public IQueryAst SelectExpression(SelectExpression selectExpression)
    {
        return selectExpression;
    }

    [Production("nodeClassReference: LPAREN identifier RPAREN")]
    public IQueryAst NodeClassReferenceExpression(
        Token<QueryToken> discardLparen,
        Identifier tableIdentifier,
        Token<QueryToken> discardRparen)
    {
        return new NodeClassReference(tableIdentifier);
    }

    [Production("nodeClassReference: LPAREN identifier COLON identifier RPAREN")]
    public IQueryAst NodeClassReferenceExpression(
        Token<QueryToken> discardLparen,
        Identifier alias,
        Token<QueryToken> discardColon,
        Identifier tableIdentifier,
        Token<QueryToken> discardRparen)
    {
        return new NodeClassReference(tableIdentifier, alias);
    }

    [Production("identifier: IDENTIFIER")]
    public IQueryAst Identifier(Token<QueryToken> identifierToken)
    {
        return new Identifier(identifierToken.Value);
    }
}