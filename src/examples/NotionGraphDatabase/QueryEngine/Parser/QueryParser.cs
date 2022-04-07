using NotionGraphDatabase.QueryEngine.Model;
using sly.lexer;
using sly.parser.generator;

namespace NotionGraphDatabase.QueryEngine.Parser;

public class QueryParser
{
    [Production("nodeClassReference: LPAREN IDENTIFIER COLON IDENTIFIER RPAREN")]
    public QueryAST NodeClassReferenceExpression(
        Token<QueryToken> discardLparen,
        Token<QueryToken> alias,
        Token<QueryToken> discardColon,
        Token<QueryToken> tableIdentifier,
        Token<QueryToken> discardRparen)
    {
        return new NodeClassReference();
    }
    
    [Production("primary: IDENTIFIER")]
    public QueryAST PrimaryIdentifier(Token<QueryToken> identifierToken)
    {
        return new Identifier();
    }
}