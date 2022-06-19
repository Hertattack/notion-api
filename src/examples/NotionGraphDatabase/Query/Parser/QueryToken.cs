using sly.lexer;

namespace NotionGraphDatabase.Query.Parser;

internal enum QueryToken
{
    #region keywords 1 -> 999

    [Keyword("return")] [Keyword("RETURN")]
    RETURN = 1,

    #endregion

    # region literals 1000 -> 1999

    [Lexeme(GenericToken.Identifier, IdentifierType.AlphaNumericDash)]
    IDENTIFIER = 1000,

    [Lexeme(GenericToken.String, "'", "\\")]
    STRING = 1001,

    [Lexeme(GenericToken.Int)] INT = 1003,

    [Sugar("*")] ALL_PROPERTIES = 1004,

    [Lexeme(GenericToken.Double)] DOUBLE = 1005,

    #endregion

    #region operators 2000 -> 2999

    [Sugar(">")] GREATER = 2000,

    [Sugar("-")] MINUS = 2001,

    [Sugar(".")] OBJECT_ACCESS = 2002,

    [Sugar("=")] EQUALS = 2003,

    #endregion

    #region sugar 3000 -> 3999

    [Sugar("(")] LPAREN = 3000,

    [Sugar(")")] RPAREN = 3001,

    [Sugar(":")] COLON = 3002,

    [Sugar("{")] LBRACE = 3003,

    [Sugar("}")] RBRACE = 3004,

    [Sugar(";")] SEMICOLON = 3005,

    [Sugar("[")] LSQBRACKET = 3006,

    [Sugar("]")] RSQBRACKET = 3007,

    [Sugar(",")] COMMA = 3008,

    #endregion

    #region comments 4000 -> 4999

    [Comment("//", "/*", "*/")] COMMENT = 4000,

    #endregion

    EOF = 0
}