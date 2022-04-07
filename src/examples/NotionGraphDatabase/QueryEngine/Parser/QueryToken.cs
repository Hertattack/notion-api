using sly.lexer;

namespace NotionGraphDatabase.QueryEngine.Parser;

public enum QueryToken
{
    [Sugar("(")]
    LPAREN = 1,
    
    [Sugar(")")]
    RPAREN = 2,
    
    [Sugar(":")]
    COLON = 3,
        
    [Lexeme(GenericToken.Identifier)] ALIAS = 1000,
    [Lexeme(GenericToken.Identifier)] IDENTIFIER = 1001,
    
    EOF = 0
}