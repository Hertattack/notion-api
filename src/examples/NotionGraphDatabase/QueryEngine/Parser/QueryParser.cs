﻿using NotionGraphDatabase.QueryEngine.Model;
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

    [Production("selectExpression: nodeClassReference MINUS LSQBRACKET identifier RSQBRACKET GREATER selectExpression")]
    public QueryPredicate SelectExpression(
        NodeClassReference fromExpression,
        Token<QueryToken> discardMinus,
        Token<QueryToken> discardLsqBracket,
        Identifier roleIdentifier,
        Token<QueryToken> discardRsqBracket,
        Token<QueryToken> discardGreater,
        SelectExpression toExpression)
    {
        return new SelectPathExpression(fromExpression, roleIdentifier, toExpression);
    }

    [Production("nodeClassReference: LPAREN identifier RPAREN")]
    public QueryPredicate NodeClassReferenceExpression(
        Token<QueryToken> discardLparen,
        Identifier nodeIdentifier,
        Token<QueryToken> discardRparen)
    {
        return new NodeClassReference(nodeIdentifier);
    }

    [Production("nodeClassReference: LPAREN identifier COLON identifier RPAREN")]
    public QueryPredicate NodeClassReferenceExpression(
        Token<QueryToken> discardLparen,
        Identifier alias,
        Token<QueryToken> discardColon,
        Identifier nodeIdentifier,
        Token<QueryToken> discardRparen)
    {
        return new NodeClassReference(nodeIdentifier, alias);
    }

    [Production("nodeClassReference: LPAREN identifier LBRACE filterExpressions RBRACE RPAREN")]
    public QueryPredicate NodeClassReferenceExpression(
        Token<QueryToken> discardLparen,
        Identifier nodeIdentifier,
        Token<QueryToken> discardLbrace,
        FilterExpressionList filterExpressionList,
        Token<QueryToken> discardRbrace,
        Token<QueryToken> discardRparen)
    {
        return new NodeClassReference(nodeIdentifier, nodeIdentifier, filterExpressionList);
    }

    [Production("nodeClassReference: LPAREN identifier COLON identifier LBRACE filterExpressions RBRACE RPAREN")]
    public QueryPredicate NodeClassReferenceExpression(
        Token<QueryToken> discardLparen,
        Identifier alias,
        Token<QueryToken> discardColon,
        Identifier nodeIdentifier,
        Token<QueryToken> discardLbrace,
        FilterExpressionList filterExpressionList,
        Token<QueryToken> discardRbrace,
        Token<QueryToken> discardRparen)
    {
        return new NodeClassReference(nodeIdentifier, alias, filterExpressionList);
    }

    [Production("filterExpressions: filterExpression")]
    public QueryPredicate FilterExpressions(FilterExpression filterExpression)
    {
        return new FilterExpressionList(filterExpression);
    }

    [Production("filterExpressions: filterExpression COMMA filterExpressions")]
    public QueryPredicate FilterExpressions(
        FilterExpression filterExpression,
        Token<QueryToken> discardComma,
        FilterExpressionList filterExpressions)
    {
        return new FilterExpressionList(filterExpressions.Expressions.Prepend(filterExpression));
    }

    [Production("filterExpression: identifier EQUALS expression")]
    public QueryPredicate FilterExpression(
        Identifier propertyIdentifier,
        Token<QueryToken> discardEquals,
        Expression expression)
    {
        return new FilterExpression(propertyIdentifier, expression);
    }

    [Production("expression: INT")]
    public QueryPredicate IntValueExpression(Token<QueryToken> intValue)
    {
        return new IntValue(intValue.IntValue);
    }

    [Production("expression: STRING")]
    public QueryPredicate StringValueExpression(Token<QueryToken> stringValue)
    {
        return new StringValue(stringValue.StringWithoutQuotes);
    }

    [Production("expression: identifier OBJECT_ACCESS identifier")]
    public QueryPredicate Expression(
        Identifier nodeName,
        Token<QueryToken> discardObjectAccess,
        Identifier propertyName)
    {
        return new PropertyIdentifier(nodeName, propertyName);
    }

    [Production("identifier: IDENTIFIER")]
    public QueryPredicate Identifier(Token<QueryToken> identifierToken)
    {
        return new Identifier(identifierToken.Value);
    }
}