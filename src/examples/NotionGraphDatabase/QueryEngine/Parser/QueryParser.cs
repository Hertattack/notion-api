using NotionGraphDatabase.QueryEngine.Model;
using sly.lexer;
using sly.parser.generator;

namespace NotionGraphDatabase.QueryEngine.Parser;

internal class QueryParser
{
    [Production("query: selectExpression")]
    public QueryPredicate QueryExpression(SelectExpression selectExpression)
    {
        var returnSpecification = new ReturnAllSpecification();
        return new QueryExpression(selectExpression, returnSpecification);
    }

    [Production("query: selectExpression returnSpecification")]
    public QueryPredicate QueryExpression(SelectExpression selectExpression, ReturnSpecification returnSpecification)
    {
        return new QueryExpression(selectExpression, returnSpecification);
    }

    [Production("returnSpecification: RETURN propertySelector")]
    public QueryPredicate ReturnSpecification(Token<QueryToken> returnStatement, PropertySelector selector)
    {
        return new ReturnSpecification(selector);
    }

    [Production("propertySelector: identifier OBJECT_ACCESS ALL_PROPERTIES")]
    public QueryPredicate PropertySelector(Identifier identifier, Token<QueryToken> objectAccessToken,
        Token<QueryToken> allPropertiesToken)
    {
        return new SelectAllProperties(identifier);
    }

    [Production("selectExpression: nodeClassReference")]
    public QueryPredicate SelectExpression(SelectExpression selectExpression)
    {
        return selectExpression;
    }

    [Production("nodeClassReference: LPAREN identifier RPAREN")]
    public QueryPredicate NodeClassReferenceExpression(
        Token<QueryToken> discardLparen,
        Identifier tableIdentifier,
        Token<QueryToken> discardRparen)
    {
        return new NodeClassReference(tableIdentifier);
    }

    [Production("nodeClassReference: LPAREN identifier COLON identifier RPAREN")]
    public QueryPredicate NodeClassReferenceExpression(
        Token<QueryToken> discardLparen,
        Identifier alias,
        Token<QueryToken> discardColon,
        Identifier tableIdentifier,
        Token<QueryToken> discardRparen)
    {
        return new NodeClassReference(tableIdentifier, alias);
    }

    [Production("identifier: IDENTIFIER")]
    public QueryPredicate Identifier(Token<QueryToken> identifierToken)
    {
        return new Identifier(identifierToken.Value);
    }
}